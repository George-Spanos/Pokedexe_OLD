using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Model;
using PokedexChat.Data;
using Model.Extensions;

namespace PokedexChat.Features.Chat {

    public class NewMessageBase : ComponentBase {
        [Inject]
        private IMessageService MessageService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        protected readonly NewMessageForm NewMessageForm = new();
        protected async void Submit()
        {
            var message = new Message((await AuthenticationState).User.ToAppUser(), NewMessageForm.Text, DateTime.Now);
            MessageService.BroadcastMessage(message);
        }
    }
}
