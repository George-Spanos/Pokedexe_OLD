using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MessageBus.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
namespace MessageBus.Features {

    [ApiController]
    [Route("[action]")]
    public class MessageStorageController : ControllerBase {

        private readonly ILogger<MessageStorageController> _logger;
        private readonly IMessageStoreService _messageStore;
        public MessageStorageController(ILogger<MessageStorageController> logger, IMessageStoreService messageStore)
        {
            _logger = logger;
            _messageStore = messageStore;
        }

        [HttpGet] public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var messages = (await _messageStore.RetrieveAsync()).Select(m => new Message
            {
                Text = m.Text,
                Timestamp = m.Timestamp,
                UserSub = m.UserSub
            });
            return Ok(messages);
        }
    }
}
