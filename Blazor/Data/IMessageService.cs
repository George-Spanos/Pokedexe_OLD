using System.Collections.Generic;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void SendMessage(Message message);
        public IReadOnlyCollection<IReadOnlyCollection<Message>> GetMessages();
    }

}
