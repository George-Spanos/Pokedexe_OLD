using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IUserDataService {
        Task InsertUserAsync(User user);
        Task<IEnumerable<User>> RetrieveUsersAsync();
    }
}
