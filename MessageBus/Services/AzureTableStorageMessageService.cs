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
            var messages = await Client.QueryAsync<TableMessage>()
                .OrderBy(message => message.Timestamp)
                .Take(10).ToListAsync();
            return messages;
        }
        public async Task InsertOrUpdateAsync(TableMessage tableMessage)
        {
            await Client.AddEntityAsync(tableMessage);
        }

    }
}
