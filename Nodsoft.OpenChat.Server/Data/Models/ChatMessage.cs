namespace Nodsoft.OpenChat.Server.Data.Models;

/// <summary>
/// Represents a message in chat.
/// </summary>
/// <typeparam name="TUserId">User ID Type</typeparam>
public class ChatMessage<TUserId> : IIdentifier<Guid> where TUserId : unmanaged
{
	/// <summary>
	/// ID of message
	/// </summary>
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; init; }

	/// <summary>
	/// ID of message's author
	/// </summary>
	public TUserId UserId { get; init; }

	/// <summary>
	/// Author of the message
	/// </summary>
	public virtual ChatUser<TUserId>? User { get; init; }

	/// <summary>
	/// Message content
	/// </summary>
	public string Content { get; set; } = string.Empty;
}
