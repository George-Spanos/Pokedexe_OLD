using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using Proto;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {
        [Parameter]
        public ImmutableList<Message> Messages { get; set; }

        [Parameter]
        public string UserEmail { get; set; }
    }
}
