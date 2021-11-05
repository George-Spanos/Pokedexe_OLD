using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Grpc.Core;
using MessageBus.Common;
using Microsoft.Extensions.Logging;
using Proto;

namespace MessageBus.Services {

    internal sealed class MessageService : Proto.MessageService.MessageServiceBase {
        private readonly ILogger<MessageService> _logger;
        private readonly IMessageStoreService _messageStore;
        private readonly Subject<Message> _chatMessageSubject = new();
        public MessageService(ILogger<MessageService> logger, IMessageStoreService messageStore)
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
                UserSub = m.UserSub
            });
            var messageList = new MessageList();
            messageList.Value.Add(messages);
            return messageList;
        }
        public override async Task<EMPTY> BroadcastMessage(Message message, ServerCallContext context)
        {
            var tableMessage = new TableMessage()
            {
                Text = message.Text,
                UserSub = message.UserSub,
                PartitionKey = "1",
                RowKey = Guid.NewGuid().ToString()
            };
            await _messageStore.InsertOrUpdateAsync(tableMessage);
            _chatMessageSubject.OnNext(message);
            return new EMPTY();
        }
        public override async Task GetNewMessage(EMPTY request, IServerStreamWriter<Message> responseStream, ServerCallContext context)
        {
            while (context.CancellationToken.IsCancellationRequested){
                await Task.Delay(1000);
                await _chatMessageSubject.Do(message => responseStream.WriteAsync(message)).ToTask();
            }

        }
    }
}
