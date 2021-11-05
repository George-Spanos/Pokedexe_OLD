using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBus.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proto;
namespace MessageBus.Features {

    [ApiController]
    [Route("[action]")]
    public class MessageBusController : ControllerBase {

        private readonly ILogger<MessageBusController> _logger;
        private readonly IMessageStoreService _messageStore;
        public MessageBusController(ILogger<MessageBusController> logger, IMessageStoreService messageStore)
        {
            _logger = logger;
            _messageStore = messageStore;
        }

        [HttpGet] public async Task<ActionResult<MessageList>> GetMessages()
        {
            var messages = (await _messageStore.RetrieveAsync()).Select(m => new Message()
            {
                Id = m.RowKey,
                Text = m.Text,
                Timestamp = m.Timestamp.ToString(),
                UserSub = m.UserSub
            });
            var messageList = new MessageList();
            messageList.Value.Add(messages);
            return messageList;
        }

        [HttpPost] public async Task<IActionResult> BroadcastMessage(Message message)
        {
             var tableMessage = new TableMessage()
            {
                Text = message.Text,
                UserSub = message.UserSub,
                PartitionKey = "1",
                RowKey = Guid.NewGuid().ToString()
            };
            await _messageStore.InsertOrUpdateAsync(tableMessage);
            return Ok();
        }
    }
}
