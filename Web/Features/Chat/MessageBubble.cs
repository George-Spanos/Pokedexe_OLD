using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Model;
using PokedexChat.Data;
namespace PokedexChat.Features.Chat {
    public class MessageBubbleBase : ComponentBase {

        [Inject]
        protected IDataService DataService { get; set; }

        [Parameter]
        public IList<Message> Messages { get; set; }

        protected User _user { get; set; }


        protected override void OnInitialized()
        {
            _user = DataService.Users.Single(user => user.Sub == Messages.First().UserSub);
        }
    }
}
