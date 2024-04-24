using Microsoft.AspNetCore.SignalR;

namespace TestConceptPattern
{
	public class SignalRHub : Hub
	{
		private int _counter = 0;
		public SignalRHub()
		{
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			return base.OnDisconnectedAsync(exception);
		}
		public Task SendAsync(string data) 
		{
			var httpContext = Context.GetHttpContext();
			Console.WriteLine(data + ":		"+ _counter + "			ConnectionId:	" + Context.ConnectionId +"		" + Context.UserIdentifier	);
			_counter++;
			Clients.All.SendAsync("ReceiveMessage", "get it with counter: "+ _counter);
			return Task.CompletedTask;
		}
	}
}
