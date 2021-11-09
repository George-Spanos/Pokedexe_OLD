using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Model;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {

    public class NewMessageBase : ComponentBase {
        [Inject]
        private IDataService DataService { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        protected NewMessageForm NewMessageForm = new();
        protected async void Submit()
        {

            var user = (await AuthenticationState).User.ToAppUser();
            if (string.IsNullOrEmpty(NewMessageForm.Text)) return;
            var message = new Message
            {
                Text = NewMessageForm.Text,
                Timestamp = DateTimeOffset.Now,
                UserSub = user.Sub
            };
            await DataService.BroadcastMessageAsync(message);
            NewMessageForm = new NewMessageForm();
            StateHasChanged();
            await FocusInput();
        }
        private async Task FocusInput()
        {
            await Js.InvokeVoidAsync("focusInput", "new-message");
        }
    }
}
