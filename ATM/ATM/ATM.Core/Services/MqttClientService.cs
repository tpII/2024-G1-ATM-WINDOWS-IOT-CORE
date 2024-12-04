using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using ATM.Core.Hubs;

namespace ATM.Core.Services
{
    public class MqttClientService : IHostedService
    {
        private IMqttClient _mqttClient;
        private readonly IHubContext<MessageHub> _hubContext;
        
        public MqttClientService(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId("RaspberryClient")
                .WithTcpServer("localhost", 1883)
                .Build();

            _mqttClient = new MqttFactory().CreateMqttClient();

            _mqttClient.ApplicationMessageReceivedAsync += async (e) =>
            {
                ArraySegment<byte> segment = e.ApplicationMessage.PayloadSegment;

                // Convertir a cadena asumiendo codificación UTF-8
                string text = System.Text.Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
                Console.WriteLine($"Mensaje recibido en el topic '{e.ApplicationMessage.Topic}': {text}");

                // Aquí notificamos a SignalR para actualizar la interfaz
                // await _hubContext.Clients.All.SendAsync("ReceiveMessage", payload);
                if (text == "Error")
                {
                    Console.WriteLine($"Hola soy Error");
                    // Enviar notificación a los clientes conectados
                    await _hubContext.Clients.All.SendAsync("NavigateToView", "Error");
                }
                else if (text == "Privacy")
                {
                    Console.WriteLine($"Hola soy Privacy");
                    await _hubContext.Clients.All.SendAsync("NavigateToView", "Privacy");
                }
            };
            
            await _mqttClient.ConnectAsync(options, cancellationToken);

            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic("CARD_VALIDATION_RESPONSE")
                .Build(), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _mqttClient.DisconnectAsync();
        }
    }
}