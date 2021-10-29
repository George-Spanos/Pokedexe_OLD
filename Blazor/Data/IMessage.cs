using System;
using System.Security.Claims;
using System.Security.Principal;
namespace PokedexChat.Data {
    internal interface IMessage {
        public IUser User { get; set; }

        public string Text { get; set; }
        
        public DateTime Timestamp { get; set; }
    }

}
