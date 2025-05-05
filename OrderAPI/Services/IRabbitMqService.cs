using OrderCommonModels.Models;
using System.Threading.Tasks; 

namespace OrdemApi.Services
{
    public interface IRabbitMqService
    {
        Task PublishOrderAsync(Ordem ordem);
    }
}
