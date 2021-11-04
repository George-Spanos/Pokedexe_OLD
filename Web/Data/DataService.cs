using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Proto;
namespace PokedexChat.Data {
    internal sealed class DataService : IDataService {

        private readonly MessageService.MessageServiceClient _messageService;
        private readonly UserManager.UserManagerClient _userManager;

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public DataService(IConfiguration configuration)
        {
            var httpHandler = new HttpClientHandler();

            var messageChannel = GrpcChannel.ForAddress(configuration.GetSection("MessageBus:Uri").Value,
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(httpHandler)
            });
            var userManagerChannel = GrpcChannel.ForAddress(configuration.GetSection("UserManager:Uri").Value,
            new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(httpHandler)
            });
            _messageService = new MessageService.MessageServiceClient(messageChannel);
            _userManager = new UserManager.UserManagerClient(userManagerChannel);
        }
        public async Task BroadcastMessage(Message message)
        {
            await _messageService.BroadcastMessageAsync(message);
        }
        public async Task UpsertUser(User user)
        {
            await _userManager.InsertUserAsync(user);
        }
        public async Task InitializeAsync()
        {
            await GetMessages();
            await GetUsers();
            await SubscribeToNewMessages();
        }
        private async Task GetUsers()
        {
            var users = await _userManager.RetrieveUsersAsync(new EMPTY());
            Users = users.Users.ToList();
        }

        private async Task GetMessages()
        {
            var messageResponse = await _messageService.GetMessagesAsync(new EMPTY());
            Messages = messageResponse.Value.Select(m => new Message(m));
        }
        private async Task SubscribeToNewMessages()
        {
            using var call = _messageService.GetNewMessage(new EMPTY());
            while (await call.ResponseStream.MoveNext(CancellationToken.None)){
                var unused = Messages.Append(call.ResponseStream.Current);
            }
        }

    }
}
