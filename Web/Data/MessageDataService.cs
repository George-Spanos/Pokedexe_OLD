using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
namespace PokedexChat.Data {
    public class MessageDataService : IMessageDataService {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MessageDataService> _logger;
        private readonly IConfiguration _configuration;
        private HubConnection _connection;

        public ISubject<Message> OnNewMessage { get; private set; }

        public MessageDataService(HttpClient httpClient, ILogger<MessageDataService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;

        }

        public async Task DisposeConnection()
        {
            await _connection.DisposeAsync();
            OnNewMessage = new Subject<Message>();
        }
        public async Task InitializeConnection()
        {
            _connection = new HubConnectionBuilder().WithUrl($"{_configuration["MessageBus:Uri"]}/chat").Build();
            OnNewMessage = new Subject<Message>();
            _connection.On<Message>(ChatEvent.OnNewMessage,
            (message) => {
                OnNewMessage.OnNext(message);
            });
            _connection.KeepAliveInterval = TimeSpan.FromHours(2);
            await _connection.StartAsync();
        }
        public async Task BroadcastMessageAsync(Message message)
        {
            if (_connection.State == HubConnectionState.Disconnected){
                await _connection.StartAsync();
            }
            await _connection.SendAsync(ChatEvent.BroadcastMessage, message);

        }
        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Message>>($"{_configuration["MessageBus:Uri"]}/GetMessages");
        }
    }
}
