using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Services
{
    public class MqttService
    {
        private readonly IMqttClient _client;
        private MqttClientOptions _options;

        public MqttService()
        {
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();
        }

        public async Task ConnectAsync()
        {
            _options = new MqttClientOptionsBuilder()
                .WithClientId("RaspberryPi_ATM")
                .WithTcpServer("localhost", 1883)
                .Build();


            _client.ApplicationMessageReceivedAsync += e =>
            {
                // Este evento se ejecuta cada vez que se recibe un mensaje

                var topic = e.ApplicationMessage.Topic;
                var payload = e.ApplicationMessage.PayloadSegment.ToString();
                Console.WriteLine($"Mensaje recibido en el topic '{topic}': {payload}");

                return Task.CompletedTask;
            };

            await _client.ConnectAsync(_options);
        }

        public async Task PublishAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _client.PublishAsync(message);
        }
    }

}