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

        protected Model.MessageList Messages { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Messages = await MessageService.GetMessages();
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
    }
}
