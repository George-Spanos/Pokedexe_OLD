using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using MessageBus.Common;
using Microsoft.Extensions.Options;
namespace MessageBus.Services {
    internal sealed class AzureTableStorageMessageService : IMessageStoreService {
        private readonly StorageConfiguration _configuration;
        public AzureTableStorageMessageService(IOptions<StorageConfiguration> options)
        {
            _configuration = options.Value;
        }
        private TableClient GetTableClient()
        {

            var tableClient = new TableClient(
            new Uri(_configuration.AccountUri),
            _configuration.TableName,
            new TableSharedKeyCredential(_configuration.AccountName, _configuration.AccountKey));
            return tableClient;
        }
        public async Task<IEnumerable<ITableMessage>> RetrieveAsync()
        {
            var messages = await GetTableClient().QueryAsync<TableMessage>().ToListAsync();
            return messages;
        }
        public Task InsertOrUpdateAsync(ITableMessage tableMessage)
        {
            throw new NotImplementedException();
        }
    }
}
