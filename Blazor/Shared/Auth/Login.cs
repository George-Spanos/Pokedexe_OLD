using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
namespace PokedexChat.Shared.Auth {
    public partial class Login: ComponentBase {

        private string Username { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private SignOutSessionStateManager SignOutManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async void OnInitialized()
        {
            Username = (await AuthenticationStateTask).User.Identity?.Name;
        }

        protected async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
    }
}
