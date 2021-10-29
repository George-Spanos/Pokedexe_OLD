namespace PokedexChat.Shared {
    public interface IUser {
        string Name { get; set; }

        string PictureUrl { get; set; }
    }

    public class User : IUser {

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }

}
