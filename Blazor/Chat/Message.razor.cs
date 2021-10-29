using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PokedexChat.Shared;
namespace PokedexChat.Chat {
    public class MessageBase : ComponentBase {
        [Parameter]
        public IUser User { get; set; }
        [Parameter]
        public string Message { get; set; }
    }
}
