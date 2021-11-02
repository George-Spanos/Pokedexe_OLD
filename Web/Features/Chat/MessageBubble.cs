using System.Collections.Immutable;
using System.Linq;
using Microsoft.AspNetCore.Components;
using PokedexChat.Data;
using Proto;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {
        [Inject]
        private IDataService _dataService { get; set; }

        [Parameter]
        public ImmutableList<Message> Messages { get; set; }

        protected User _user { get; set; }


        protected override void OnInitialized()
        {
            _user = _dataService.Users.Single(user => user.Email == Messages.First().UserEmail);
        }
    }
}
