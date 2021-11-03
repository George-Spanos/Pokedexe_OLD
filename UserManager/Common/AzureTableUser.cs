using System;
using Azure;
namespace UserManager.Common {
    public class AzureTableUser : ITableUser {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string Email { get; set; }

        public string PictureUrl { get; set; }

        public string Name { get; set; }
    }
}
