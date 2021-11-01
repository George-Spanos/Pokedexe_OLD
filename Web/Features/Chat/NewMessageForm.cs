using System.ComponentModel.DataAnnotations;
namespace PokedexChat.Features.Chat {
    public class NewMessageForm {
        [Required]
        public string Text { get; set; }
    }
}
