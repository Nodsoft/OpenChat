namespace Nodsoft.OpenChat.Server.Data.Models;

public class ChatUser<TId> : IIdentifier<TId> where TId : unmanaged
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public TId Id { get; init; }

	public string Username { get; init; } = string.Empty;
}
