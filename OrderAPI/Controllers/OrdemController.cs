using Microsoft.AspNetCore.Mvc;
using OrdemApi.Services;
using OrdemApi.Repositories;
using OrderCommonModels.Models;

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

            using var stream = new StreamReader(file.OpenReadStream());
            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var parts = line.Split(';');

                if (parts.Length < 4)
                    continue;

                var ordem = new Ordem
                {
                    TipoOrdem = parts[0],
                    NomeAtivo = parts[1],
                    Preco = decimal.Parse(parts[2]),
                    Quantidade = int.Parse(parts[3])
                };

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
