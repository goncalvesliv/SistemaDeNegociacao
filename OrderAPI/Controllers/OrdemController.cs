using Microsoft.AspNetCore.Mvc;
using OrdemAPI.Models;
using OrdemApi.Services;

namespace OrdemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdemController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public OrdemController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
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
                    continue; // Ignora linhas inválidas

                var ordem = new Ordem
                {
                    TipoOrdem = parts[0],
                    NomeAtivo = parts[1],
                    Preco = decimal.Parse(parts[2]),
                    Quantidade = int.Parse(parts[3])
                };

                await _rabbitMqService.PublishOrderAsync(ordem); // <<< agora é await
            }

            return Ok("Arquivo processado e ordens enviadas!");
        }
    }
}
