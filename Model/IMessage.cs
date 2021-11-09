using System;
namespace Model {
    public interface IMessage {
        string UserSub { get; set; }

        string Text { get; set; }

        DateTimeOffset? Timestamp { get; set; }
    }
}
