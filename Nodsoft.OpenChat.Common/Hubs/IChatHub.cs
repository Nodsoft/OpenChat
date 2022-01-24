using Nodsoft.OpenChat.Common.Data.DTOs;

namespace Nodsoft.OpenChat.Common.Hubs;

/// <summary>
/// Chat hub methods pushed by server.
/// </summary>
/// <typeparam name="TUserId">User ID type</typeparam>
public interface IChatHubPush<TUserId> where TUserId : notnull
{
	/// <summary>
	/// Pushes a new message to the clients.
	/// </summary>
	/// <param name="message">New message</param>
	/// <returns></returns>
	public Task NewMessage(ChatMessageDTO<TUserId> message);

	/// <summary>
	/// Pushes a message edit to the clients. 
	/// </summary>
	/// <param name="message">Updated message</param>
	/// <returns></returns>
	public Task EditMessage(ChatMessageDTO<TUserId> message);

	/// <summary>
	/// Signals a message deletion to the clients.
	/// </summary>
	/// <param name="id">ID of deleted message</param>
	/// <returns></returns>
	public Task DeleteMessage(Guid id);

	/// <summary>
	/// Signals to all clients that a user has joined chat.
	/// </summary>
	/// <param name="user">Details of user who joined</param>
	/// <returns></returns>
	public Task UserJoined(ChatUserDTO<TUserId> user);

	/// <summary>
	/// Signalls to all clients that a user has left chat.
	/// </summary>
	/// <param name="userId">ID of user who left</param>
	/// <returns></returns>
	public Task UserLeft(TUserId userId);
}

/// <summary>
/// Chat hub methods invoked by client.
/// </summary>
public interface IChatHubInvoke
{
	/// <summary>
	/// Sends a new message to the chat room.
	/// </summary>
	/// <param name="content">Content of the message</param>
	/// <returns></returns>
	public Task<Guid> SendNewMessage(string content);

	/// <summary>
	/// Edits an existing message in the chat room.
	/// </summary>
	/// <param name="id">ID of the message</param>
	/// <param name="content">Content to be replaced with</param>
	/// <returns></returns>
	public Task EditMessage(Guid id, string content);

	/// <summary>
	/// Deletes a message in the chat room.
	/// </summary>
	/// <param name="id">ID of the message</param>
	/// <returns></returns>
	public Task DeleteMessage(Guid id);
}
