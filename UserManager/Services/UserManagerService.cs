using System;
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
                    userList.Users.Add(new User()
                    {
                        Sub = user.Sub,
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
        public override async Task<EMPTY> InsertUser(User user, ServerCallContext context)
        {
            try{
                await _userStore.InsertOrUpdateAsync(user);
                return new EMPTY();
            }
            catch (RpcException exception){
                const string message = "Failed to Upsert User";
                _logger.LogError(exception, message);
                throw new InvalidOperationException(message);
            }
        }
    }
}
