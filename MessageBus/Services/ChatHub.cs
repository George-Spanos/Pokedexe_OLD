using System;
using System.Threading.Tasks;
using MessageBus.Common;
using Microsoft.AspNetCore.SignalR;
using Model;
namespace MessageBus.Services {
    public class ChatHub : Hub, IChatHub {
        private readonly IMessageStoreService _messageStore;
        public ChatHub(IMessageStoreService messageStore)
        {
            _messageStore = messageStore;
        }
        public async Task BroadcastMessage(Message message)
        {
            var tableMessage = new TableMessage()
            {
                Text = message.Text,
                UserSub = message.UserSub,
                PartitionKey = "1",
                RowKey = Guid.NewGuid().ToString()
            };
            await _messageStore.InsertOrUpdateAsync(tableMessage);
            await Clients.All.SendAsync("NewMessage", message);
        }
    }
}
