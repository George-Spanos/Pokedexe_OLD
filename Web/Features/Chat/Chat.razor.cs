﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PokedexChat.Data;
using PokedexChat.Extensions;
namespace PokedexChat.Features.Chat {
    public class ChatBase : ComponentBase {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationState).User.ToAppUser();
            /*  TODO 
             *  Upsert user fails because it needs to be refactored to match every user,
             *  with the "sub" property, not the email.
             */

            // await DataService.UpsertUser(user);
        }
    }
}
