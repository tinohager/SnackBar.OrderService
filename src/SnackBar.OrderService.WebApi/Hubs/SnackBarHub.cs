using Microsoft.AspNetCore.SignalR;
using SnackBar.OrderService.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace SnackBar.OrderService.WebApi.Hubs
{
    public class SnackBarHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var name = Context.GetHttpContext().Request.Query["name"];
            await Clients.All.SendAsync($"{name} joined the chat");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.GetHttpContext().Request.Query["name"];
            await Clients.All.SendAsync($"{name} left the chat");
        }

        public async Task CreateOrder(ArticleOrder[] articles)
        {
            await Clients.Others.SendAsync("NewOrder", articles);
        }
    }
}
