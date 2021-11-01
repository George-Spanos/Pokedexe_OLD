namespace MessageBus.Common {
    public interface ITableMessage {
        string RowKey { get; set; }

        string Text { get; set; }

        string UserEmail { get; set; }
        string Timestamp { get; set; }
    }

}
