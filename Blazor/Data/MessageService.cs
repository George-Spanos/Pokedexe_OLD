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

        private GrpcChannel channel;
        private Proto.MessageService.MessageServiceClient _client;
        private readonly List<Message> _messages = new();
        public MessageService()
        {
            channel = GrpcChannel.ForAddress("https://localhost:8080",
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });
            _client = new Proto.MessageService.MessageServiceClient(channel);
        }
        public void BroadcastMessage(Message message)
        {
            Console.WriteLine($"{message.User.Name} wrote {message.Text}");
        }
        public Task<MessageList> GetMessages()
        {
            var response = _client.GetMessages(new Proto.EMPTY());
            return Task.FromResult(MessageList
                .Create(response.Messages.Select(m => new Message(new User(m.User.Name, m.User.PictureUrl), m.Text, m.Timestamp.ToDateTime()))));

        }
        public Task<IMessage> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
