using PokedexChat.Data.MQTT;
namespace PokedexChat.Data {
    public class StreamingService {
        private readonly IMqttClientService _mqttClientService;
        public StreamingService(MqttClientServiceProvider provider)
        {
            _mqttClientService = provider.MqttClientService;
        }
    }
}
