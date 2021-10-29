using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
namespace PokedexChat.Data {
    public class User : RemoteUserAccount, IUser {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public User(string name, string pictureUrl)
        {
            Name = name;
            PictureUrl = pictureUrl;
        }
    }
}
