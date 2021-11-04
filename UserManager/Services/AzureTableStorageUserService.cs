using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Proto;
using UserManager.Common;
namespace UserManager.Services {
    internal sealed class AzureTableStorageUserService : IUserStoreService<AzureTableUser> {
        private readonly ILogger<AzureTableStorageUserService> _logger;
        private readonly StorageConfiguration _configuration;

        public AzureTableStorageUserService(ILogger<AzureTableStorageUserService> logger, IOptions<StorageConfiguration> option)
        {
            _logger = logger;
            _configuration = option.Value;
        }
        private TableClient GetTableClient()
        {

            var tableClient = new TableClient(
            new Uri(_configuration.AccountUri),
            _configuration.TableName,
            new TableSharedKeyCredential(_configuration.AccountName, _configuration.AccountKey));
            return tableClient;
        }

        public async Task<IEnumerable<AzureTableUser>> RetrieveAsync()
        {
            var users = await GetTableClient().QueryAsync<AzureTableUser>().ToListAsync();
            return users;
        }
        public async Task<Result<bool>> InsertOrUpdateAsync(User user)
        {
            var tableClient = GetTableClient();
            var tableUser = await tableClient.QueryAsync<AzureTableUser>($"Email eq {user.Email}").FirstOrDefaultAsync();

            if (tableUser != null){
                _logger.LogInformation("User exists");
                var response = await tableClient.UpdateEntityAsync(tableUser, ETag.All, TableUpdateMode.Replace);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
            else{
                _logger.LogInformation("New User!");
                var newTableUser = new AzureTableUser()
                {
                    Email = user.Email,
                    Name = user.Name,
                    PictureUrl = user.PictureUrl
                };
                var response = await tableClient.AddEntityAsync(newTableUser);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
        }

    }
}
