using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using UserManager.Common;
namespace UserManager.Services {
    internal sealed class AzureTableStorageUserService : IUserStoreService<AzureTableUser> {
        private readonly StorageConfiguration _configuration;

        public AzureTableStorageUserService(IOptions<StorageConfiguration> option)
        {
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
        public async Task<Result<bool>> InsertOrUpdateAsync(AzureTableUser tableUser)
        {
            var tableClient = GetTableClient();
            var user = await tableClient.QueryAsync<AzureTableUser>(filter: $"Email eq {tableUser.UserEmail}").FirstOrDefaultAsync();

            if (user != null){
                var response = await tableClient.UpdateEntityAsync(tableUser, ETag.All, TableUpdateMode.Replace);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
            else{
                var response = await tableClient.AddEntityAsync(tableUser);
                return Result<bool>.Success(response.Status.Equals(HttpStatusCode.OK));
            }
        }

    }
}
