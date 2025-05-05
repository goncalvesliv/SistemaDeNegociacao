using Microsoft.AspNetCore.Mvc;
using OrdemApi.Services;
using OrdemApi.Repositories;
using OrderCommonModels.Models;
using System.Globalization;

namespace OrdemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdemController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly NegociacoesRepository _negociacoesRepository; 

        public OrdemController(IRabbitMqService rabbitMqService, NegociacoesRepository negociacoesRepository)
        {
            _rabbitMqService = rabbitMqService;
            _negociacoesRepository = negociacoesRepository; 
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            using var reader = new StreamReader(file.OpenReadStream());
            string linha;
            int linhaAtual = 0;

            var ordens = new List<Ordem>();

            while ((linha = await reader.ReadLineAsync()) != null)
            {
                linhaAtual++;
                var partes = linha.Split(';');

                if (partes.Length < 4)
                    return BadRequest($"Linha {linhaAtual} está com formato inválido. Esperado: TipoOrdem;NomeAtivo;Preco;Quantidade");

                var tipoOrdem = partes[0];
                var nomeAtivo = partes[1];
                var precoStr = partes[2];
                var quantidadeStr = partes[3];

                if (!decimal.TryParse(precoStr, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal preco) ||
                    !int.TryParse(quantidadeStr, out int quantidade))
                {
                    return BadRequest($"Linha {linhaAtual} contém dados inválidos (preço ou quantidade).");
                }

                var ordem = new Ordem
                {
                    TipoOrdem = tipoOrdem,
                    NomeAtivo = nomeAtivo,
                    Preco = preco,
                    Quantidade = quantidade,
                    DataCriacao = DateTime.UtcNow
                };

                ordens.Add(ordem);
            }

            foreach (var ordem in ordens)
            {
                await _rabbitMqService.PublishOrderAsync(ordem);
            }

            return Ok("Arquivo processado e ordens enviadas!");
        }


        [HttpGet("ordens-processadas")]
        public async Task<ActionResult<List<OrdemProcessada>>> GetOrdensProcessadas()
        {
            try
            {
                var ordens = await _negociacoesRepository.ObterOrdensProcessadas();
                return Ok(ordens);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar ordens processadas: {ex.Message}");
            }
        }

        [HttpGet("negocios")]
        public async Task<ActionResult<List<Negocio>>> GetNegocios()
        {
            try
            {
                var negocios = await _negociacoesRepository.ObterNegocios();
                return Ok(negocios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar negócios: {ex.Message}");
            }
        }


    }
}
