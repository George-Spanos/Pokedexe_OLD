using System;
namespace Model {
    public class Message : IMessage {
        public string UserSub { get; set; }

        public string Text { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
    }
}
