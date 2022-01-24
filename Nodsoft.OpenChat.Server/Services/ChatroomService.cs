using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nodsoft.OpenChat.Common.Data.DTOs;
using Nodsoft.OpenChat.Common.Hubs;
using Nodsoft.OpenChat.Server.Data;
using Nodsoft.OpenChat.Server.Data.Models;
using Nodsoft.OpenChat.Server.Hubs;
using System.Collections.Concurrent;
using System.Linq;

namespace Nodsoft.OpenChat.Server.Services;


public class ChatroomService<TUserId> where TUserId : notnull
{
	private readonly DbContext _context;
	private readonly IHubContext<ChatHub<TUserId>, IChatHubPush<TUserId>> _hub;

	private static readonly ConcurrentDictionary<TUserId, ChatUser<TUserId>> _connectedUsers = new();
	public IEnumerable<ChatUser<TUserId>> ConnectedUsers => _connectedUsers.Values;

	public ChatroomService(IOpenChatDbContext dbContext, IHubContext<ChatHub<TUserId>, IChatHubPush<TUserId>> hub)
	{
		_context = dbContext as DbContext ?? throw new ArgumentException($"{dbContext.GetType()} does not implement {nameof(DbContext)}.", nameof(dbContext));
		_hub = hub;
	}

	public Task OnUserJoinedAsync(ChatUserDTO<TUserId> userDTO)
	{
		ChatUser<TUserId> entity = userDTO.Adapt<ChatUser<TUserId>>();

		if (GetChatUsers().Find(userDTO.Id) is null)
		{
			GetChatUsers().Add(entity);

		}
		else
		{
			GetChatUsers().Update(entity);
		}

		_context.SaveChanges();

		_connectedUsers.TryAdd(entity.Id, entity);
		return _hub.Clients.All.UserJoined(userDTO);
	}

	public Task OnUserLeftAsync(TUserId userId)
	{
		_connectedUsers.TryRemove(userId, out _);
		return _hub.Clients.All.UserLeft(userId);
	}

	public async Task<Guid> SubmitMessageAsync(ChatMessageDTO<TUserId> message)
	{
		if (!_connectedUsers.ContainsKey(message.UserId))
		{
			throw new InvalidOperationException($"Cannot submit message. User {message.UserId} is not connected to chatbox.");
		}

		ChatMessage<TUserId> entity = GetChatMessages().Add(message.Adapt<ChatMessage<TUserId>>()).Entity;
		_context.SaveChanges();

		await _hub.Clients.All.NewMessage(entity.Adapt<ChatMessageDTO<TUserId>>());
		return entity.Id;
	}

	public Task EditMessageAsync(ChatMessageDTO<TUserId> updated)
	{
		if (!_connectedUsers.ContainsKey(updated.UserId))
		{
			throw new InvalidOperationException($"Cannot edit message. User {updated.UserId} is not connected to chatbox.");
		}

		ChatMessage<TUserId>? current = GetChatMessages().Find(updated.Id) ?? throw new KeyNotFoundException($"Message not found with ID {updated.Id}.");
		current.Content = updated.Content;

		_context.SaveChanges();

		return _hub.Clients.All.EditMessage(updated.Adapt<ChatMessageDTO<TUserId>>());
	}

	public Task DeleteMessageAsync(Guid id)
	{
		ChatMessage<TUserId>? msg = GetChatMessages().Find(id) ?? throw new KeyNotFoundException($"Message not found with ID {id}.");

		GetChatMessages().Remove(msg);
		_context.SaveChanges();
		return _hub.Clients.All.DeleteMessage(id);
	}

	private DbSet<ChatUser<TUserId>> GetChatUsers() => _context.Set<ChatUser<TUserId>>()
		?? throw new ArgumentException($"{_context.GetType()} doesn't have a DbSet of type {typeof(ChatUser<TUserId>)}.", nameof(_context));

	private DbSet<ChatMessage<TUserId>> GetChatMessages() => _context.Set<ChatMessage<TUserId>>()
		?? throw new ArgumentException($"{_context.GetType()} doesn't have a DbSet of type {typeof(ChatMessage<TUserId>)}.", nameof(_context));
}
