namespace Model {
    public class Message : IMessage {
        public string UserSub { get; set; }

        public string Text { get; set; }

        public string Timestamp { get; set; }
    }
}
