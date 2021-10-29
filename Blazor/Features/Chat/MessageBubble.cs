using Microsoft.AspNetCore.Components;
using PokedexChat.Data;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {
        [Parameter]
        public Message Message { get; set; }
    }
}
