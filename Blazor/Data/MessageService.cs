using System;
using System.Collections.Generic;
using System.Linq;
namespace PokedexChat.Data {
    public class MessageService : IMessageService {
        private readonly List<Message> _messages = new();

        private IEnumerable<Message> Messages => _messages;

        public void SendMessage(Message message)
        {
            Console.WriteLine($"{message.User.Name} wrote {message.Text}");
        }
        public IReadOnlyList<IReadOnlyList<Message>> GetMessages()
        {
            var user = new User("George Spanos", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLcs3igV0QK_ErQ4ub7yNUsBbiv9-YS0Lj4A&usqp=CAU");
            for (var i = 0; i < 10; i++){
                var newMessage = new Message(user, $"I sent a message {i}", DateTime.Now);
                _messages.Add(newMessage);
            }
            return _messages
                .GroupBy(message => message.User.Name)
                .Select(grp => grp.ToList())
                .ToList();
        }
    }
}
