namespace HappyThoughts.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HappyThoughts.Services.Data.Messages;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IMessagesService messagesService;

        public ChatHub(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, Text = message, });
        }
    }

    public class Message
    {
        public string User { get; set; }

        public string Text { get; set; }
    }
}
