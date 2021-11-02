using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MessageBus.Common;
using Microsoft.Extensions.Logging;
using Proto;

namespace MessageBus.Services {

    internal sealed class MessageService : Proto.MessageService.MessageServiceBase {
        private readonly ILogger<MessageService> _logger;
        private readonly IMessageStoreService _messageStore;
        internal MessageService(ILogger<MessageService> logger, IMessageStoreService messageStore)
        {
            _logger = logger;
            _messageStore = messageStore;
        }
        public override async Task<MessageList> GetMessages(EMPTY request, ServerCallContext context)
        {
            var messages = (await _messageStore.RetrieveAsync()).Select(m => new Message()
            {
                Id = m.RowKey,
                Text = m.Text,
                Timestamp = m.Timestamp.ToString(),
                UserEmail = m.UserEmail
            });
            var messageList = new MessageList();
            messageList.Value.Add(messages);
            return messageList;
        }
    }
}
