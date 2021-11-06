using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
namespace PokedexChat.Data {
    public class UserDataService : IUserDataService {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserDataService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _endpointUrl;
        public UserDataService(HttpClient httpClient, ILogger<UserDataService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _endpointUrl = _configuration["UserManager:Uri"];
        }
        public async Task InsertUserAsync(User user)
        {
            var userJson = JsonSerializer.Serialize(user);
            var requestContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_endpointUrl}/UpsertUser", requestContent);
            response.EnsureSuccessStatusCode();
        }
        public async Task<IEnumerable<User>> RetrieveUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"{_endpointUrl}/RetrieveUsers");
        }
    }
}
