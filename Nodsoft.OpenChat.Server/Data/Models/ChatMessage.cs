namespace Nodsoft.OpenChat.Server.Data.Models;

public class ChatMessage<TUserId> : IIdentifier<Guid> where TUserId : unmanaged
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; init; }

	public TUserId UserId { get; init; }
	public virtual ChatUser<TUserId>? User { get; init; }
}
