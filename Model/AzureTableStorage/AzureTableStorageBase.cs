using System;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
namespace Model.AzureTableStorage {
    public abstract class AzureTableStorageBase {
        private readonly IConfiguration _configuration;
        protected AzureTableStorageBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected TableClient Client
        {
            get
            {
                var tableClient = new TableClient(
                new Uri(_configuration["StorageAccountUri"]),
                _configuration["StorageTableName"],
                new TableSharedKeyCredential(_configuration["StorageAccountName"], _configuration["StorageAccountKey"]));
                return tableClient;
            }
        }
    }
}
