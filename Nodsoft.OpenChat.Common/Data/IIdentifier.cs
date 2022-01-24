namespace Nodsoft.OpenChat.Common;

/// <summary>
/// Represents a key identifiable object.
/// </summary>
/// <typeparam name="TId">Type of ID</typeparam>
public interface IIdentifier<TId> where TId : notnull
{
	public TId Id { get; init; }
}
