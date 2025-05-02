using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using OrderCommonModels.Models;

namespace OrderProcessor.Services
{
    public class RabbitMqConsumer
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqConsumer()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public async Task StartAsync(Func<Ordem, Task> handleMessage)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "ordem", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var ordem = JsonSerializer.Deserialize<Ordem>(json);

                if (ordem != null)
                {
                    await handleMessage(ordem);
                }
            };

            await channel.BasicConsumeAsync(queue: "ordem", autoAck: true, consumer: consumer);

            Console.WriteLine(" [*] Aguardando mensagens...");
        }
    }
}
