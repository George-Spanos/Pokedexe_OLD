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
        private IMessageService MessageService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        protected readonly NewMessageForm NewMessageForm = new();
        protected async void Submit()
        {
            var user = (await AuthenticationState).User.ToAppUser();
            var message = new Message()
            {
                Text = NewMessageForm.Text,
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                User = new User()
                {
                    Name = user.Name,
                    PictureUrl = user.PictureUrl
                }
            };
            MessageService.BroadcastMessage(message);
        }
    }
}
