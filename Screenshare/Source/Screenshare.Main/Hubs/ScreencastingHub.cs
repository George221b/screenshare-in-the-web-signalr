using Microsoft.AspNet.SignalR;
using Screenshare.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Screenshare.Main.Hubs
{
	public class ScreencastingHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}

        public async Task ReceiveBody(string masterGuid)
        {
            if (GlobalCollections.users.ContainsKey(masterGuid))
            {
                Master client = GlobalCollections.users[masterGuid];

                await Clients.Caller.receiveInitialMasterBody(client.Body, client.Head, client.Width, client.Height);
            }
        }

        public override Task OnConnected()
        {
            var masterUuid = Context.QueryString["uuid"];
            var isMaster = Context.QueryString["master"];

            if (GlobalCollections.users.ContainsKey(masterUuid))
            {
                if (isMaster == "true")
                {
                    GlobalCollections.users[masterUuid].ConnectionID = Context.ConnectionId;
                }
                else
                {
                    GlobalCollections.users[masterUuid].Slaves.Add(new Slave
                    {
                        ConnectionID = Context.ConnectionId
                    });
                }
            }

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var masterUuid = Context.QueryString["uuid"];
            var isMaster = Context.QueryString["master"];
            if (isMaster == "false")
            {
                if (GlobalCollections.users.ContainsKey(masterUuid))
                {
                    Master master = GlobalCollections.users[masterUuid];
                    Slave slaveClient = master.Slaves.Where(c => c.ConnectionID == Context.ConnectionId).FirstOrDefault();
                    if (slaveClient != null)
                        master.Slaves.Remove(slaveClient);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}