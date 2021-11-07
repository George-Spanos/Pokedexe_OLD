using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {
    public class ChatBase : ComponentBase {
        protected bool Initialized { get; private set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationState).User.ToAppUser();
            await DataService.InitializeAsync();
            await DataService.InsertUserAsync(user);
            Initialized = true;
        }
    }
}
