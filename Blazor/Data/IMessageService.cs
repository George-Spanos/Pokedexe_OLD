using System.Threading.Tasks;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void SendMessage(Message message);
    }

}
