using System.Linq;
namespace PokedexChat.Data {
    public interface IUser {
        string Name { get; set; }

        string PictureUrl { get; set; }
        public string Firstname => Name.Split(" ").First();
    }
}
