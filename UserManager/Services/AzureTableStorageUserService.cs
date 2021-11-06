using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using SharedKernel.AzureTableStorage;
using UserManager.Common;
namespace UserManager.Services {
    internal sealed class AzureTableStorageUserService : AzureTableStorageBase, IUserStoreService<AzureTableUser> {
        private readonly ILogger<AzureTableStorageUserService> _logger;

        public async Task<IEnumerable<AzureTableUser>> RetrieveAsync()
        {
            var users = await Client.QueryAsync<AzureTableUser>().ToListAsync();
            return users;
        }
        public async Task<Result<bool>> InsertOrUpdateAsync(User user)
        {
            var tableUser = await Client.QueryAsync<AzureTableUser>($"Sub eq '{user.Sub}'").FirstOrDefaultAsync();

            if (tableUser != null){
                _logger.LogInformation("User exists");
                var response = await Client.UpdateEntityAsync(tableUser, ETag.All, TableUpdateMode.Replace);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
            else{
                _logger.LogInformation("New User!");
                var newTableUser = new AzureTableUser
                {
                    Sub = user.Sub,
                    Name = user.Name,
                    PictureUrl = user.PictureUrl,
                    PartitionKey = "1"
                };
                var response = await Client.AddEntityAsync(newTableUser);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
        }

        public AzureTableStorageUserService(IConfiguration configuration, ILogger<AzureTableStorageUserService> logger) : base(configuration)
        {
            _logger = logger;
        }
    }
}
