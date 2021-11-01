using System;
using Azure;
namespace MessageBus.Common {
    public class TableMessage : ITableMessage {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string Text { get; set; }

        public string UserEmail { get; set; }
    }
}
