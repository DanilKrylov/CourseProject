using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace BSTeamSearch.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DBContent _db;
        public ChatHub(DBContent db)
        {
            _db = db;
        }

        public async Task Send(string message, string groupId)
        {
            var name = Context.GetHttpContext().Session.GetString("name");
            if (message is null || message == string.Empty || name is null)
            {
                return;
            }

            _db.Messages.Add(new Message(message, int.Parse(groupId), name));
            _db.SaveChanges();
            var time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            await Clients.Group(groupId.ToString()).SendAsync("Send", message, time, name);
        }

        public async Task Enter(string groupId)
        {
            var name = Context.GetHttpContext().Session.GetString("name");
            if (name is null)
            {
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task RemoveFromChat(string groupId)
        {
            var name = Context.GetHttpContext().Session.GetString("name");
            if (name is null)
            {
                return;
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }
    }
}
