using System;
using System.Linq;
using System.Security.Claims;
namespace Model.Extensions {
    public static class UserExtensions {
        public static IUser ToAppUser(this ClaimsPrincipal user)
        {
            var name = user.Identity?.Name;
            Console.WriteLine("User claims are");
            foreach (var claim in user.Claims){
                Console.WriteLine($"{claim.Type} - {claim.Value}");
            }
            var pictureUrl = user.Claims.Single(claim => claim.Type == "picture").Value;
            return new User(name, pictureUrl);
        }
    }
}
