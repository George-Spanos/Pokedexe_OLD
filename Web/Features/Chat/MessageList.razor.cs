using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PokedexChat.Data;
using Proto;
namespace PokedexChat.Features.Chat {

    public class MessageListBase : ComponentBase {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [Inject]
        private IDataService DataService { get; set; }

        protected IEnumerable<Message> Messages { get; private set; }

        protected override void OnInitialized()
        {
            Messages = DataService.Messages;
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
    }
}
