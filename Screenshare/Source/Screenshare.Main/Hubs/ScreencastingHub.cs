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

        public async Task SaveInitialMasterData(string masterGuid, string body, string head, int width, int height)
		{
            Master masterClient = GlobalCollections.users[masterGuid];
            masterClient.Body = body;
            masterClient.Head = head;
            masterClient.Width = width;
            masterClient.Height = height;

            await Task.CompletedTask;
        }

        public async Task UpdateSlaveBody(string masterGuid, string body)
        {
            Master masterClient = GlobalCollections.users[masterGuid];
            masterClient.Body = body;

            foreach (Slave hubClient in masterClient.Slaves)
            {
                var client = Clients.Client(hubClient.ConnectionID);

                await client.receiveMasterBody(body, masterClient.Width, masterClient.Height);
            }
        }

        public async Task UpdateSlaveScroll(string masterGuid, int scrollPositionX, int scrollPositionY)
        {
            Master masterClient = GlobalCollections.users[masterGuid];
            masterClient.ScrollTop = scrollPositionY;
            masterClient.ScrollLeft = scrollPositionX;

            foreach (Slave hubClient in masterClient.Slaves)
            {
                var client = Clients.Client(hubClient.ConnectionID);

                await client.receiveMasterScroll(scrollPositionX, scrollPositionY);
            }
        }

        public async Task UpdateSlaveMousePosition(string masterGuid, int mousePositionX, int mousePositionY)
        {
            Master masterClient = GlobalCollections.users[masterGuid];
            masterClient.CursorTop = mousePositionY;
            masterClient.CursorLeft = mousePositionX;
            foreach (Slave hubClient in masterClient.Slaves)
            {
                var client = Clients.Client(hubClient.ConnectionID);

                await client.receiveMasterMouse(mousePositionX, mousePositionY);
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