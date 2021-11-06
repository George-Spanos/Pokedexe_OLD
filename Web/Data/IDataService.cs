using System.Collections;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IDataService {

        IEnumerable<Message> Messages { get; set; }

        IEnumerable<User> Users { get; set; }

        Task BroadcastMessageAsync(Message message);

        ISubject<Message> OnNewMessage { get; }

        Task InsertUserAsync(User user);
        Task InitializeAsync();
        Task Dispose();
    }
}
