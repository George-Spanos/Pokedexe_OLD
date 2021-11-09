using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Model;
using Model.Extensions;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {

    public class MessageListBase : OwningComponentBase<IDataService> {

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }

        protected List<List<Message>> Messages { get; private set; }

        private IDisposable Subscription { get; set; }

        protected override void OnInitialized()
        {
            if (DataService.Messages != null && DataService.Messages.Any()){
                Messages = DataService.Messages
                    .GroupWhile((next, previous) => next.UserSub == previous.UserSub)
                    .Select(m => m.ToList())
                    .ToList();
            }
            Subscription = DataService.OnNewMessage.Do(message => Console.WriteLine($"New Message {message.Text}")).Subscribe(async (message) => {
                var lastMessageBubble = Messages.LastOrDefault();
                if (lastMessageBubble != null && lastMessageBubble.First().UserSub == message.UserSub){
                    lastMessageBubble.Add(message);
                }
                else{
                    Messages.Add(new List<Message> { message });
                }

                if ((await AuthenticationState).User.UserSub() != message.UserSub){
                    await Js.InvokeVoidAsync("notify");
                }
                StateHasChanged();
            });
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            await Js.InvokeVoidAsync("scrollToBottom");
        }
        protected override void Dispose(bool disposing)
        {
            DataService.Dispose();
            Subscription.Dispose();
        }
    }
}
