using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Proto;
using UserManager.Common;
namespace UserManager.Services {
    public class UserManagerService : Proto.UserManager.UserManagerBase {
        private readonly ILogger<UserManagerService> _logger;
        private readonly IUserStoreService<AzureTableUser> _userStore;

        public UserManagerService(ILogger<UserManagerService> logger, IUserStoreService<AzureTableUser> userStore)
        {
            _logger = logger;
            _userStore = userStore;
        }
        public override async Task<UserList> RetrieveUsers(EMPTY request, ServerCallContext context)
        {
            try{
                var userList = new UserList();
                foreach (var user in await _userStore.RetrieveAsync()){
                    var json = JsonSerializer.Serialize(user);
                    Console.Write(json);
                    _logger.LogDebug(json);
                    userList.Users.Add(new User()
                    {
                        Email = user.UserEmail,
                        Name = user.Name,
                        PictureUrl = user.PictureUrl
                    });
                }
                return userList;
            }
            catch (RpcException exception){
                _logger.LogError(exception, "Failed to Fetch Users from Storage");
                throw new InvalidOperationException("Failed to Fetch Users from Storage");
            }
        }
    }
}
