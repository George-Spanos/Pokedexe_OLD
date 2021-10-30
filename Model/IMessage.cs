using System;
namespace Model {
    public interface IMessage {
        public IUser User { get; set; }

        public string Text { get; set; }
        
        public DateTime Timestamp { get; set; }
    }

}
