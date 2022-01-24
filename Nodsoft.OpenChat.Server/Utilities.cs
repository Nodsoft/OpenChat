using Microsoft.AspNetCore.SignalR;
using Nodsoft.OpenChat.Common;
using Nodsoft.OpenChat.Common.Data.DTOs;
using System.Security.Claims;

namespace Nodsoft.OpenChat.Server;

public static class Utilities
{
	public static ChatUserDTO<TUserId> GetCurrentUser<TUserId>(this HubCallerContext context) where TUserId : notnull => new(
			context.UserIdentifier.TryParse<TUserId>() ?? throw new InvalidCastException($"Could not parse to type {typeof(TUserId)}."),
			context.User.FindFirst(ClaimTypes.Name)?.Value);
}
