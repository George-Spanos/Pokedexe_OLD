using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using Model.Proto;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {
        [Parameter]
        public ImmutableList<Message> Messages{ get; set; }
        [Parameter]
        public User User { get; set; }
    }
}
