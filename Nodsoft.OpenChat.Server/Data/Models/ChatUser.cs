using Nodsoft.OpenChat.Common;

namespace Nodsoft.OpenChat.Server.Data.Models;

/// <summary>
/// Represents a user in chat.
/// </summary>
/// <typeparam name="TId">User ID type</typeparam>
public record ChatUser<TId> : IIdentifier<TId> where TId : notnull
{
	/// <summary>
	/// ID of chat user
	/// </summary>
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public TId Id { get; init; } = default!;

	/// <summary>
	/// Display name of chat user
	/// </summary>
	public string? Username { get; init; }
}
