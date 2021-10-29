using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
namespace PokedexChat.Chat {
    public class NewMessageForm {
        [Required]
        public string Text { get; set; }
    }

    public class NewMessageBase : ComponentBase {

        protected readonly NewMessageForm NewMessageForm = new();
        protected void Submit()
        {
            Console.WriteLine(NewMessageForm.Text);
        }
    }
}
