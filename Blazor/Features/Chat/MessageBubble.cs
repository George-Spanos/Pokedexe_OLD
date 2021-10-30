using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Model;
using PokedexChat.Data;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {
        [Parameter]
        public IReadOnlyCollection<Message> Messages{ get; set; }
        [Parameter]
        public IUser User { get; set; }
    }
}
