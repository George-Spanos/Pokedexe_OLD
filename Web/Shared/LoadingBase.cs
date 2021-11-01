using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
namespace PokedexChat.Shared {
    public class LoadingBase : ComponentBase {
        protected bool IsLoaded;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationStateTask;
            IsLoaded = true;
        }
    }
}
