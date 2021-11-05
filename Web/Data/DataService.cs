using System.Collections.Generic;
using Proto;
namespace PokedexChat.Data {
    public sealed class DataService : IDataService {

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IMessageDataService MessageDataService { get; set; }

        public IUserDataService UserDataService { get; set; }

        public DataService(IMessageDataService messageDataService, IUserDataService userDataService)
        {
            MessageDataService = messageDataService;
            UserDataService = userDataService;

        }


    }
}
