using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public sealed class DataService : IDataService {

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<User> Users { get; set; }

        private IMessageDataService MessageDataService { get; }

        private IUserDataService UserDataService { get; }

        public DataService(IMessageDataService messageDataService, IUserDataService userDataService)
        {
            MessageDataService = messageDataService;
            UserDataService = userDataService;

        }
        public async Task BroadcastMessageAsync(Message message)
        {
            await MessageDataService.BroadcastMessageAsync(message);
        }

        public ISubject<Message> OnNewMessage => MessageDataService.OnNewMessage;


        public async Task InsertUserAsync(User user)
        {
            var _ = Users.Append(user);
            await UserDataService.InsertUserAsync(user);
        }
        public async Task InitializeAsync()
        {
            Messages = (await MessageDataService.GetMessagesAsync());
            Users = (await UserDataService.RetrieveUsersAsync());
            await MessageDataService.InitializeConnection();
        }
        public async Task Dispose()
        {
            await MessageDataService.DisposeConnection();
        }


    }
}
