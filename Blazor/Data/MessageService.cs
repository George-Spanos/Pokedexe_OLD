using System;
namespace PokedexChat.Data {
    public class MessageService : IMessageService {

        public void SendMessage(Message message)
        {
            Console.WriteLine($"{message.User.Name} wrote {message.Text}");
        }
    }
}
