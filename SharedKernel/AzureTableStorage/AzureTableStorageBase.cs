using System;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
namespace SharedKernel.AzureTableStorage {
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
                new Uri(_configuration["StorageCredentials:AccountUri"]),
                _configuration["StorageCredentials:TableName"],
                new TableSharedKeyCredential(_configuration["StorageCredentials:AccountName"], _configuration["StorageCredentials:AccountKey"]));
                return tableClient;
            }
        }
    }
}
