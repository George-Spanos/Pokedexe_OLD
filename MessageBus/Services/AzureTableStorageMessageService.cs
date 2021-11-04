using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBus.Common;
using Microsoft.Extensions.Configuration;
using SharedKernel.AzureTableStorage;
namespace MessageBus.Services {
    internal sealed class AzureTableStorageMessageService : AzureTableStorageBase, IMessageStoreService {
        public AzureTableStorageMessageService(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IEnumerable<ITableMessage>> RetrieveAsync()
        {
            var messages = await Client.QueryAsync<TableMessage>().ToListAsync();
            return messages;
        }
        public Task InsertOrUpdateAsync(ITableMessage tableMessage)
        {
            throw new NotImplementedException();
        }

    }
}
