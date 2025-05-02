using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using OrdemApi.Services;
using OrderCommonModels.Models;

namespace OrdemApi.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqService()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public async Task PublishOrderAsync(Ordem ordem)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "ordem",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var json = JsonSerializer.Serialize(ordem);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "",
                                            routingKey: "ordem",
                                            body: body);
        }
    }
}
