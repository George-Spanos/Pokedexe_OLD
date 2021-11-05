using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proto;
namespace PokedexChat.Data {
    public interface IDataService {

        IEnumerable<Message> Messages { get; set; }

        IEnumerable<User> Users { get; set; }

        IMessageDataService MessageDataService { get; set; }

        IUserDataService UserDataService { get; set; }
    }
}
