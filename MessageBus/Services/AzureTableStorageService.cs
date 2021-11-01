using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using MessageBus.Common;
namespace MessageBus.Services {
    public class AzureTableStorageService : IMessageStoreService {

        private static TableClient GetTableClient()
        {
            var tableClient = new TableClient(
            new Uri("https://pokedexestorage.table.core.windows.net"),
            "Messages",
            new TableSharedKeyCredential("pokedexestorage", "9nx8W58vEFpMywuxiPP63VDSC2Y8NEGmo/8gKL/S1uz/d8kl8ZHQdQ6IdtaAAnvrM2eWfX1Jwnt4RTb69eBFpQ=="));
            return tableClient;
        }
        public async Task<IEnumerable<ITableMessage>> RetrieveAsync()
        {
            var messages = await GetTableClient().QueryAsync<TableMessage>().ToListAsync();
            return messages;
        }
        public Task InsertOrUpdateAsync(ITableMessage tableMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
