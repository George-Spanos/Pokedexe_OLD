using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
namespace PokedexChat.Data {
    public class MessageDataService : IMessageDataService {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MessageDataService> _logger;
        private readonly IConfiguration _configuration;
        public MessageDataService(HttpClient httpClient, ILogger<MessageDataService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }
        public Task BroadcastMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Message>>($"{_configuration["MessageBus:Uri"]}/GetMessages");
        }
    }
}
