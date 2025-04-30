using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderProcessor.Services;
using OrdemAPI.Models;

namespace OrderProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqConsumer _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _consumer = new RabbitMqConsumer();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(ProcessarOrdem);
        }

        private async Task ProcessarOrdem(Ordem ordem)
        {
            _logger.LogInformation($"Recebido: {ordem.TipoOrdem} | {ordem.NomeAtivo} | {ordem.Preco} | {ordem.Quantidade}");

            // ⚠️ Aqui vai a lógica de casamento das ordens (compra vs venda)
            // Por enquanto só loga. Na próxima etapa podemos implementar a lógica real.

            await Task.CompletedTask;
        }
    }
}
