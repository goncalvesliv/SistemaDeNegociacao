using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderCommonModels.Models;
using OrderProcessor.Services;

namespace OrderProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqConsumer _consumer;
        private readonly NegocioRepository _repository;

        private readonly List<Ordem> _ordensCompra = new();
        private readonly List<Ordem> _ordensVenda = new();

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _consumer = new RabbitMqConsumer();

            var connectionString = configuration.GetConnectionString("NegociacoesDb");
            _repository = new NegocioRepository(connectionString);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(ProcessarOrdem);
        }

        private async Task ProcessarOrdem(Ordem novaOrdem)
        {
            _logger.LogInformation($"Recebido: {novaOrdem.TipoOrdem} | {novaOrdem.NomeAtivo} | {novaOrdem.Preco} | {novaOrdem.Quantidade}");

            int quantidadeOriginal = novaOrdem.Quantidade;
            var negocios = new List<Negocio>();
            var listaOposta = novaOrdem.TipoOrdem == "C" ? _ordensVenda : _ordensCompra;

            var ordensCompativeis = listaOposta
                .Where(o => o.NomeAtivo == novaOrdem.NomeAtivo && o.Preco == novaOrdem.Preco)
                .OrderBy(o => o.Quantidade)  
                .ThenBy(o => o.TipoOrdem)   
                .ToList();

            foreach (var ordemExistente in ordensCompativeis)
            {
                if (novaOrdem.Quantidade == 0)
                    break;

                var quantidadeNegociada = Math.Min(novaOrdem.Quantidade, ordemExistente.Quantidade);

                negocios.Add(new Negocio
                {
                    NomeAtivo = novaOrdem.NomeAtivo,
                    Preco = novaOrdem.Preco,
                    Quantidade = quantidadeNegociada
                });

                novaOrdem.Quantidade -= quantidadeNegociada;
                ordemExistente.Quantidade -= quantidadeNegociada;
            }

            if (novaOrdem.Quantidade > 0)
            {
                var listaDestino = novaOrdem.TipoOrdem == "C" ? _ordensCompra : _ordensVenda;
                listaDestino.Add(novaOrdem);
            }

            if (negocios.Any())
            {
                await _repository.SalvarNegociosAsync(negocios);
                _logger.LogInformation($"{negocios.Count} negócio(s) salvo(s) no banco.");
            }
            else
            {
                _logger.LogInformation("Nenhum negócio foi gerado para essa ordem.");
            }
           
            int quantidadeExecutada = negocios
                .Where(n => n.NomeAtivo == novaOrdem.NomeAtivo && n.Preco == novaOrdem.Preco)
                .Sum(n => n.Quantidade);

            
            string status;
            if (quantidadeExecutada == 0)
                status = "Ativa";
            else if (quantidadeExecutada < quantidadeOriginal)
                status = "Parcialmente Executada";
            else
                status = "Finalizada";

            var ordemProcessada = new OrdemProcessada
            {
                TipoOrdem = novaOrdem.TipoOrdem,
                NomeAtivo = novaOrdem.NomeAtivo,
                Preco = novaOrdem.Preco,
                Quantidade = quantidadeOriginal,
                Status = status
            };


            
            await _repository.SalvarOrdemAsync(ordemProcessada);

        }
    }
}
