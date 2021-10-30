using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void SendMessage(Message message);
        public Task<MessageList> GetMessages();
        public Task<IMessage> GetNewMessage();
    }

}
