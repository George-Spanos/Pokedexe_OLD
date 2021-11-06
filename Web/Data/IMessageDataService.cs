using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IMessageDataService {
        Task BroadcastMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesAsync();
    }
}
