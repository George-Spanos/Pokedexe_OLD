using System;
using System.Text.Json;
using System.Threading.Tasks;
using MessageBus.Common;
using Microsoft.Extensions.Logging;
using Model;
using MQTTnet.AspNetCore.AttributeRouting;
namespace MessageBus.Features {

    [MqttController]
    public class MessageBusController : MqttBaseController {
        private readonly ILogger<MessageBusController> _logger;
        private readonly IMessageStoreService _messageStore;
        public MessageBusController(IMessageStoreService messageStore, ILogger<MessageBusController> logger)
        {
            _messageStore = messageStore;
            _logger = logger;
        }
        [MqttRoute(nameof(BroadcastMessage))] public async Task BroadcastMessage()
        {
            _logger.LogInformation("New Message Arrived");
            try{
                var messageJson = BitConverter.ToString(Message.Payload) ?? "";
                var message = JsonSerializer.Deserialize<Message>(messageJson) ?? new Message();
                _logger.LogInformation(messageJson);
                _logger.LogInformation(message.ToString());
                var tableMessage = new TableMessage()
                {
                    Text = message.Text,
                    UserSub = message.UserSub,
                    PartitionKey = "1",
                    RowKey = Guid.NewGuid().ToString()
                };
                await _messageStore.InsertOrUpdateAsync(tableMessage);
                await Ok();
            }
            catch (Exception error){
                _logger.LogError("Error on BroadCastMessage {@Error}", error);
                await BadMessage();
            }

        }
    }
}
