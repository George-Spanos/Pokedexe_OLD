using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Model.Extensions;
using PokedexChat.Data;
using Proto;
namespace PokedexChat.Features.Chat {

    public class MessageListBase : OwningComponentBase<IDataService> {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        private IDataService DataService { get; set; }

        protected ImmutableList<ImmutableList<Message>> Messages { get; private set; }

        protected override void OnInitialized()
        {
            DataService = ScopedServices.GetRequiredService<IDataService>();
            
            Messages = DataService.Messages
                .GroupWhile((next, previous) => next.UserEmail == previous.UserEmail)
                .Select(m => m.ToImmutableList())
                .ToImmutableList();
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
    }
}
