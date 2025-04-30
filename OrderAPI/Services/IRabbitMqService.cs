using OrdemAPI.Models;
using System.Threading.Tasks; // não esqueça

namespace OrdemApi.Services
{
    public interface IRabbitMqService
    {
        Task PublishOrderAsync(Ordem ordem);
    }
}
