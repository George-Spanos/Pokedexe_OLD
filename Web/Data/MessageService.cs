using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Proto;
using MessageList=Model.MessageList;
namespace PokedexChat.Data {
    internal sealed class MessageService : IMessageService {

        private readonly GrpcChannel channel;
        private readonly Proto.MessageService.MessageServiceClient _client;
        private readonly IConfiguration _configuration;
        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
            var httpHandler = new HttpClientHandler();

            channel = GrpcChannel.ForAddress(_configuration.GetSection("ApiUrl").Value,
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(httpHandler)
            });
            _client = new Proto.MessageService.MessageServiceClient(channel);
        }
        public void BroadcastMessage(Message message)
        {
            Console.WriteLine($"{message.UserEmail} wrote {message.Text}");
        }
        public async Task<MessageList> GetMessages()
        {
            var messageResponse = await _client.GetMessagesAsync(new EMPTY());
            var messages = messageResponse.Value.Select(m => new Message(m));
            return MessageList.Create(messages);
        }
        public Task<Message> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
