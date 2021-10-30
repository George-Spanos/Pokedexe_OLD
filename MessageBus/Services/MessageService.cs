using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Model;
using Model.Proto;
using MessageList=Model.Proto.MessageList;
namespace MessageBus.Services {

    public class MessageService : Model.Proto.MessageService.MessageServiceBase {
        private const string MockApiUrl = "https://617c98a31eadc500171362d5.mockapi.io/Messages";
        private readonly ILogger<MessageService> _logger;
        private readonly HttpClient _http;
        public MessageService(ILogger<MessageService> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
        }
        public override async Task<MessageList> GetMessages(EMPTY request, ServerCallContext context)
        {
            var messages = await _http.GetFromJsonAsync<IEnumerable<Model.Proto.Message>>(MockApiUrl);
            var messageList = new MessageList();
            messageList.Messages.Add(messages);
            return messageList;
        }
    }
}
