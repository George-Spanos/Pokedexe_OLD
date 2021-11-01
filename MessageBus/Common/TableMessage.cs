namespace MessageBus.Common {
    public class TableMessage : ITableMessage {
        public string RowKey { get; set; }

        public string Text { get; set; }

        public string UserEmail { get; set; }

        public string Timestamp { get; set; }

        public TableMessage()
        {

        }
    }
}
