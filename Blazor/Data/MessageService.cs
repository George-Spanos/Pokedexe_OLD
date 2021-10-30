using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Proto;
namespace PokedexChat.Data {
    internal sealed class MessageService : IMessageService {

        private readonly GrpcChannel channel;
        private readonly Proto.MessageService.MessageServiceClient _client;
        public MessageService()
        {
            var httpHandler = new HttpClientHandler();

            channel = GrpcChannel.ForAddress("https://localhost:8080",
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(httpHandler)
            });
            _client = new Proto.MessageService.MessageServiceClient(channel);
        }
        public void BroadcastMessage(Message message)
        {
            Console.WriteLine($"{message.User.Name} wrote {message.Text}");
        }
        public async Task<Model.MessageList> GetMessages()
        {
            var messageResponse = await _client.GetMessagesAsync(new EMPTY());
            var messages = messageResponse.Value.Select(m => new Message(m));
            return Model.MessageList.Create(messages);
        }
        public Task<Message> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
