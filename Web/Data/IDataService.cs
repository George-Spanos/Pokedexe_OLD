using System.Collections.Generic;
using System.Threading.Tasks;
using Proto;
namespace PokedexChat.Data {
    public interface IDataService {

        IEnumerable<User> Users { get; set; }

        IEnumerable<Message> Messages { get; set; }

        Task BroadcastMessage(Message message);

        Task UpsertUser(User user);
        Task InitializeAsync();
        void SubscribeToNewMessages();
    }
}
