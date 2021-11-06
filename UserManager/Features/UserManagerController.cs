using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using UserManager.Common;
namespace UserManager.Features {

    [ApiController]
    [Route("[action]")]
    public class UserManagerController : ControllerBase {
        private readonly ILogger<UserManagerController> _logger;
        private readonly IUserStoreService<AzureTableUser> _db;

        public UserManagerController(ILogger<UserManagerController> logger, IUserStoreService<AzureTableUser> db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet] public async Task<ActionResult<IEnumerable<User>>> RetrieveUsers()
        {
            try{
                var userList = (await _db.RetrieveAsync()).Select(user => new User
                    {
                        Sub = user.Sub,
                        Name = user.Name,
                        PictureUrl = user.PictureUrl
                    })
                    .ToList();
                return new OkObjectResult(userList);
            }
            catch (Exception exception){
                _logger.LogError(exception, "Failed to Fetch Users from Storage");
                throw new InvalidOperationException("Failed to Fetch Users from Storage");
            }
        }
        [HttpPost] public async Task<IActionResult> UpsertUser(
            User user)
        {
            try{
                await _db.InsertOrUpdateAsync(user);
                return Ok();
            }
            catch (Exception exception){
                const string message = "Failed to Upsert User";
                _logger.LogError(exception, message);
                throw new InvalidOperationException(message);
            }
        }
    }
}
