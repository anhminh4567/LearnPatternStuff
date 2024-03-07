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
			Console.WriteLine(data + ":		"+ _counter + "			ConnectionId:	" + Context.ConnectionId +"		" + Context.UserIdentifier	);
			_counter++;
			return Task.CompletedTask;
		}
	}
}
