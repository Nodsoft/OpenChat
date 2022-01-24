using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Nodsoft.OpenChat.Common;
using Nodsoft.OpenChat.Common.Data.DTOs;
using Nodsoft.OpenChat.Common.Hubs;
using Nodsoft.OpenChat.Server.Services;
using System.Security.Claims;

namespace Nodsoft.OpenChat.Server.Hubs;

[Authorize]
public class ChatHub<TUserId> : Hub<IChatHubPush<TUserId>>, IChatHubInvoke where TUserId : notnull
{
	private readonly ChatroomService<TUserId> _service;

	public ChatHub(ChatroomService<TUserId> service)
	{
		_service = service;
	}

	public override async Task OnConnectedAsync()
	{
		await _service.OnUserJoinedAsync(Context.GetCurrentUser<TUserId>());

		await base.OnConnectedAsync();
	}

	public override Task OnDisconnectedAsync(Exception exception)
	{
		_service.OnUserLeftAsync(Context.GetCurrentUser<TUserId>().Id);
		return base.OnDisconnectedAsync(exception);
	}

	public Task<Guid> SendNewMessage(string content) => _service.SubmitMessageAsync(new(default, Context.GetCurrentUser<TUserId>().Id, content));

	public Task EditMessage(Guid id, string content) => _service.EditMessageAsync(new(id, Context.GetCurrentUser<TUserId>().Id, content));

	public Task DeleteMessage(Guid id) => _service.DeleteMessageAsync(id);
}
