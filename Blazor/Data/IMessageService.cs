﻿using System.Threading.Tasks;
using Model.Proto;
namespace PokedexChat.Data {
    public interface IMessageService {
        public void BroadcastMessage(Message message);
        public Task<Model.MessageList> GetMessages();
        public Task<Message> GetNewMessage();
    }

}
