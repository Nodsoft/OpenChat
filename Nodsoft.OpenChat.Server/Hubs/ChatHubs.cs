using Microsoft.AspNetCore.SignalR;
using Nodsoft.OpenChat.Common.Hubs;

namespace Nodsoft.OpenChat.Server.Hubs;

public class ChatHubs<TUserId> : Hub<IChatHubPush<TUserId>>, IChatHubInvoke where TUserId : unmanaged
{
	public Task DeleteMessage(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task EditMessage(Guid id, string content)
	{
		throw new NotImplementedException();
	}

	public Task<Guid> SendNewMessage(string content)
	{
		throw new NotImplementedException();
	}
}
