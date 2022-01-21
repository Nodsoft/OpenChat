namespace Nodsoft.OpenChat.Server.Data.Models;

/// <summary>
/// Represents a user in chat.
/// </summary>
/// <typeparam name="TId">User ID type</typeparam>
public class ChatUser<TId> : IIdentifier<TId> where TId : unmanaged
{
	/// <summary>
	/// ID of chat user
	/// </summary>
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public TId Id { get; init; }

	/// <summary>
	/// Display name of chat user
	/// </summary>
	public string Username { get; init; } = string.Empty;
}
