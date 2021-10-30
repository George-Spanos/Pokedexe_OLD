using System.Collections.Generic;
using System.Threading.Tasks;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void SendMessage(Message message);
        public Task<MessageList> GetMessages();
    }

}
