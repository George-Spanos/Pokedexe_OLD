using System;
using System.Globalization;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Proto;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {

    public class NewMessageBase : ComponentBase {
        [Inject]
        private IDataService DataService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        protected NewMessageForm NewMessageForm = new();
        protected async void Submit()
        {

            var user = (await AuthenticationState).User.ToAppUser();
            var message = new Message()
            {
                Text = NewMessageForm.Text,
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                UserSub = user.Sub
            };
            await DataService.BroadcastMessage(message);
            NewMessageForm = new NewMessageForm();
            StateHasChanged();
        }
    }
}
