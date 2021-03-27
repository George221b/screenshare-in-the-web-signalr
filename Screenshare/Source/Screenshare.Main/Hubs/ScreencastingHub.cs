using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screenshare.Main.Hubs
{
	public class ScreencastingHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}
	}
}