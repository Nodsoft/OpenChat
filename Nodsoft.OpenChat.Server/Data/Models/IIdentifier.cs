namespace Nodsoft.OpenChat.Server.Data;

/// <summary>
/// Represents a key identifiable object.
/// </summary>
/// <typeparam name="TId">Type of ID</typeparam>
public interface IIdentifier<TId> where TId : unmanaged
{
	public TId Id { get; init; }
}
