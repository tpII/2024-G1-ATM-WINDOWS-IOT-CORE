// using System;
// using System.Threading.Tasks;
// using ATM.Core.Services;
// using ATM.Core.UseCases;

// namespace Testing
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Hello World!");
//             MqttService service = new MqttService();
//             service.ConnectAsync().Wait();
//             WithdrawUseCase use_case = new WithdrawUseCase(service);
//             try
//             {
//                 Task t = use_case.Execute(50);
//                 Console.WriteLine("Withdraw ejecutado");
//                 t.Wait();
//                 Console.WriteLine("Withdraw completado");
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.Message);
//             }
//         }
//     }
// }
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static string topic = "test/topic";

    static async Task Main(string[] args)
    {
        // Crear un cliente MQTT
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        // Configurar las opciones del cliente MQTT
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883) // Dirección del broker MQTT (puedes usar otro broker)
            .WithClientId("ConsoleClient") // ID único para este cliente
            .Build();

        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            // Este evento se ejecuta cada vez que se recibe un mensaje
            Console.WriteLine($"Mensaje recibido en el topic '{e.ApplicationMessage.Topic}': {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
            return Task.CompletedTask;
        };

        // Conectar al broker
        await mqttClient.ConnectAsync(options);

        // Suscribir al topic deseado
        await mqttClient.SubscribeAsync(topic);

        Console.WriteLine("Suscrito al topic 'test/topic'. Esperando mensajes...");
        
        // Mantener la aplicación en ejecución para seguir recibiendo mensajes
        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadLine();

        // Desconectar al final
        await mqttClient.UnsubscribeAsync(topic);
        await mqttClient.DisconnectAsync();
    }
}
