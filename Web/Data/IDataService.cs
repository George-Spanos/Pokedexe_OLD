using System.Collections.Generic;
using System.Threading.Tasks;
using Proto;
namespace PokedexChat.Data {
    public interface IDataService {

        IEnumerable<User> Users { get; set; }

        IEnumerable<Message> Messages { get; set; }

        Task<IEnumerable<Message>> GetMessages();
        Task BroadcastMessage(Message message);

        Task UpsertUser(User user);

        Task<IEnumerable<User>> GetUsers();

    }
}
