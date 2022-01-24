using System.ComponentModel.DataAnnotations;

namespace Nodsoft.OpenChat.Common.Data.DTOs;

/// <summary>
/// Represents a user in chat.
/// </summary>
/// <typeparam name="TId">User ID type</typeparam>
/// <param name="Id">ID of User</param>
/// <param name="Username">User's Display name</param>
public record ChatUserDTO<TId>(TId Id, string? Username) where TId : notnull;
