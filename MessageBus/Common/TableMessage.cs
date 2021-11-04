using System;
using Azure;
using Azure.Data.Tables;
namespace MessageBus.Common {
    internal class TableMessage : ITableMessage {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string Text { get; set; }

        public string UserSub { get; set; }

    }
}
