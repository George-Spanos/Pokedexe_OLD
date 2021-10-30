using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Model;
using PokedexChat.Extensions;
namespace PokedexChat.Data {
    internal sealed class MessageService : IMessageService {
        private readonly List<Message> _messages = new();

        public void SendMessage(Message message)
        {
            Console.WriteLine($"{message.User.Name} wrote {message.Text}");
        }
        public Task<MessageList> GetMessages()
        {
            var user = new User("George Spanos", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLcs3igV0QK_ErQ4ub7yNUsBbiv9-YS0Lj4A&usqp=CAU");
            for (var i = 0; i < 10; i++){
                var newMessage = new Message(user, $"I sent a message {i}", DateTime.Now);
                _messages.Add(newMessage);
            }
            return Task.FromResult(MessageList.Create(_messages));

        }
        public Task<IMessage> GetNewMessage()
        {
            throw new NotImplementedException();
        }
    }
}
