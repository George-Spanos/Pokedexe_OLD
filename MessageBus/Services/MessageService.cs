using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Proto;
using Google.Protobuf.WellKnownTypes;
using MessageBus.Common;
namespace MessageBus.Services {

    public class MessageService : Proto.MessageService.MessageServiceBase {
        private const string MockApiUrl = "https://617c98a31eadc500171362d5.mockapi.io/Messages";
        private readonly ILogger<MessageService> _logger;
        private readonly HttpClient _http;
        private readonly IMessageStoreService _messageStore;
        public MessageService(ILogger<MessageService> logger, HttpClient http, IMessageStoreService messageStore)
        {
            _logger = logger;
            _http = http;
            _messageStore = messageStore;
        }
        public override async Task<MessageList> GetMessages(EMPTY request, ServerCallContext context)
        {
            var messages = (await _messageStore.RetrieveAsync()).Select(m => new Message()
            {
                Id = m.RowKey,
                Text = m.Text,
                Timestamp = m.Timestamp,
            });
            var messageList = new MessageList();
            messageList.Value.Add(messages);
            return messageList;
        }
    }
}
