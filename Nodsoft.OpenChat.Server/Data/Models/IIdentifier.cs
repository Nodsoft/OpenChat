namespace Nodsoft.OpenChat.Server.Data;

public interface IIdentifier<TId> where TId : unmanaged
{
	public TId Id { get; init; }
}
