namespace AutoTrade.Web.Hubs
{
	using Microsoft.AspNetCore.SignalR;

	public class ChatHub : Hub
	{
		public async Task SendMessageToCaller(string user, string message)
		{
			await Clients.Caller.SendAsync("ReceiveMessage", user, message);
		}
	}
}
