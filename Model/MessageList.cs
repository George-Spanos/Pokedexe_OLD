using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Model.Extensions;
namespace Model {
    public partial class MessageList {
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
        public static MessageList CreateEmpty()
        {
            return new MessageList(new List<IMessage>());
        }
        public MessageList Add(Message message)
        {
            return Create(_messages.Append(message));
        }
    }
}
