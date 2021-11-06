using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Model;
namespace PokedexChat.Data {
    public interface IMessageDataService {
        ISubject<Message> OnNewMessage { get; }

        Task DisposeConnection();
        Task InitializeConnection();
        Task BroadcastMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesAsync();
    }
}
