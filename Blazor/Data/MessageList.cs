using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using PokedexChat.Extensions;
namespace PokedexChat.Data {
    public class MessageList {
        private readonly IEnumerable<IMessage> _messages;

        public ImmutableList<ImmutableList<Message>> Value => _messages
            .GroupWhile((previous, next) => previous.User.Name == next.User.Name)
            .Select(group => {
                return group.Select(message => new Message(message.User, message.Text, message.Timestamp)).ToImmutableList();
            })
            .ToImmutableList();

        private MessageList(IEnumerable<IMessage> messages)
        {
            _messages = messages.ToList().AsReadOnly();
        }
        public static MessageList Create(IEnumerable<IMessage> messages)
        {
            return new MessageList(messages);
        }
    }
}
