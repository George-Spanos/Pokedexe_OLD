using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public sealed class DataService : IDataService {

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IMessageDataService MessageDataService { get; set; }

        public IUserDataService UserDataService { get; set; }

        public async Task InitializeAsync()
        {
            Messages = (await MessageDataService.GetMessagesAsync());
            Users = (await UserDataService.RetrieveUsersAsync());
            await MessageDataService.InitializeConnection();
        }

        public DataService(IMessageDataService messageDataService, IUserDataService userDataService)
        {
            MessageDataService = messageDataService;
            UserDataService = userDataService;

        }


    }
}
