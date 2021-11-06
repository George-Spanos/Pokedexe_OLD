using System;
using System.Linq;
using System.Security.Claims;
using Model;
namespace PokedexChat.Extensions {
    public static class UserExtensions {
        public static User ToAppUser(this ClaimsPrincipal user)
        {
            var name = user.Identity?.Name;
            var sub = user.Claims.SingleOrDefault(claim => claim.Type == "sub")?.Value;
            Console.WriteLine("User claims are");
            foreach (var claim in user.Claims){
                Console.WriteLine($"{claim.Type} - {claim.Value}");
            }
            var pictureUrl = user.Claims.Single(claim => claim.Type == "picture").Value;
            var newUser = new User
            {
                Name = name,
                PictureUrl = pictureUrl,
                Sub = sub
            };
            return newUser;
        }
    }
}
