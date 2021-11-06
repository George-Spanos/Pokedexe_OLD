namespace Model {
    public interface IMessage {
        string UserSub { get; set; }

        string Text { get; set; }

        string Timestamp { get; set; }
    }
}
