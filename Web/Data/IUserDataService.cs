using System.Threading.Tasks;
using Proto;
namespace PokedexChat.Data {
    public interface IUserDataService {
        Task InsertUserAsync(User user);
        Task<UserList> RetrieveUsersAsync();
    }
}
