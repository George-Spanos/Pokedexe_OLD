using System.Threading.Tasks;
using Proto;
namespace PokedexChat.Data {
    public interface IMessageDataService {
        Task BroadcastMessageAsync(Message message);
        Task<MessageList> GetMessagesAsync();
    }
}
