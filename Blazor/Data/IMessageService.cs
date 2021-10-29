using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void SendMessage(Message message);
        public IReadOnlyList<IReadOnlyList<Message>> GetMessages();
    }

}
