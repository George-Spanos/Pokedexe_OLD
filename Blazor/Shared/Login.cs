using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
namespace PokedexChat.Shared {
    public partial class Login {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private SignOutSessionStateManager SignOutManager { get; set; }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
    }
}
