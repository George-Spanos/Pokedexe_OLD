using System.Linq;
namespace Model {
    public interface IUser {
        string Name { get; set; }

        string PictureUrl { get; set; }
        public string Firstname => Name.Split(" ").First();
    }
}
