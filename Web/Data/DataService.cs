using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Proto;
namespace PokedexChat.Data {
    internal sealed class DataService : IDataService {

        private readonly MessageService.MessageServiceClient _messageService;
        private readonly UserManager.UserManagerClient _userManager;
        public DataService(IConfiguration configuration)
        {
            var httpHandler = new HttpClientHandler();

            var channel = GrpcChannel.ForAddress(configuration.GetSection("ApiUrl").Value,
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(httpHandler)
            });
            _messageService = new MessageService.MessageServiceClient(channel);
            _userManager = new UserManager.UserManagerClient(channel);
        }
        public async Task BroadcastMessage(Message message)
        {
            await _messageService.BroadcastMessageAsync(message);
            Console.WriteLine($"{message.UserEmail} wrote {message.Text}");
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            var messageResponse = await _messageService.GetMessagesAsync(new EMPTY());
            var messages = messageResponse.Value.Select(m => new Message(m));
            return messages;
        }
        public Task<Message> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
