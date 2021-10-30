namespace Model {
    public sealed class User : IUser {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public User(string name, string pictureUrl)
        {
            Name = name;
            PictureUrl = pictureUrl;
        }
    }
}
