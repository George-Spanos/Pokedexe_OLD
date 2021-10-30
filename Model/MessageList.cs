using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Model.Extensions;
using Proto;
namespace Model {
    public class MessageList {
        private readonly IEnumerable<Message> _messages;

        public ImmutableList<ImmutableList<Message>> Value => _messages
            .GroupWhile((previous, next) => previous.User.Name == next.User.Name)
            .Select(group => {
                return group.Select(message => new Message(message)).ToImmutableList();
            })
            .ToImmutableList();

        private MessageList(IEnumerable<Message> messages)
        {
            _messages = messages.ToList().AsReadOnly();
        }
        public static MessageList Create(IEnumerable<Message> messages)
        {
            return new MessageList(messages);
        }
        public static MessageList CreateEmpty()
        {
            return new MessageList(new List<Message>());
        }
        public MessageList Add(Message message)
        {
            return Create(_messages.Append(message));
        }
    }
}
