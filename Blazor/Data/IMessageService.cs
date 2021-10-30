using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void BroadcastMessage(Message message);
        public Task<MessageList> GetMessages();
        public Task<IMessage> GetNewMessage();
    }

}
