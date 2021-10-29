using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {

    public class MessageListBase : ComponentBase {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        protected List<Message> Messages { get; } = new();

        protected override async Task OnInitializedAsync()
        {
            for (var i = 0; i < 10; i++){
                var newMessage = new Message((await AuthenticationState).User.ToAppUser(), $"I sent a message {i}", DateTime.Now);
                Messages.Add(newMessage);
            }
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
    }
}
