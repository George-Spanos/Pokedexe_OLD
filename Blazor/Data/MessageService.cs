using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Model;
using Proto=Model.Proto;
namespace PokedexChat.Data {
    internal sealed class MessageService : IMessageService {

        private readonly GrpcChannel channel;
        private readonly Proto.MessageService.MessageServiceClient _client;
        private readonly List<Message> _messages = new();
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
        public async Task<MessageList> GetMessages()
        {
            var response = await _client.GetMessagesAsync(new Proto.EMPTY());
            return MessageList.Create(response.Messages
                .Select(m => new Message(new User(m.Username, m.Picture), m.Text, DateTime.Parse(m.Timestamp))));

        }
        public Task<IMessage> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
