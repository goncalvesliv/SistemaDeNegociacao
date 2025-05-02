using Microsoft.AspNetCore.SignalR;

namespace OrderApi.Hubs
{
    public class OrdemHub : Hub
    {
        public async Task NotificarAtualizacao()
        {
            await Clients.All.SendAsync("AtualizarOrdens");
        }
    }
}
