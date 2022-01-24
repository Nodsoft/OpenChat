namespace Nodsoft.OpenChat.Common.Data.DTOs;

/// <summary>
/// Represents a message in chat
/// </summary>
/// <typeparam name="TUserId">ID Type of user</typeparam>
/// <param name="Id">ID of chat message</param>
/// <param name="UserId">User ID of chat message's author</param>
/// <param name="Content">Message content</param>
public record ChatMessageDTO<TUserId>(Guid Id, TUserId UserId, string Content) where TUserId : notnull;
