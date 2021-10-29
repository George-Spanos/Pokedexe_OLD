using System;
namespace PokedexChat.Data {
    public class Message : IMessage {
        public IUser User { get; set; }


        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public Message(IUser user, string text, DateTime timestamp)
        {
            User = user;
            Text = text;
            Timestamp = timestamp;
        }
        public static explicit operator string(Message message) => message.Text;
    }
}
