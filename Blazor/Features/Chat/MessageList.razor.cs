using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PokedexChat.Data;
namespace PokedexChat.Features.Chat {

    public class MessageListBase : ComponentBase {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [Inject]
        private IMessageService MessageService { get; set; }

        protected IReadOnlyList<IReadOnlyList<Message>> Messages { get; set; }

        protected override void OnInitialized()
        {
            Messages = MessageService.GetMessages();
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
    }
}
