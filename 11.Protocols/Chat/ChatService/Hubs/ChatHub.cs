using Data;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ChatServer.Hubs
{
    public class ChatHub: Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Установлено новое соединение {Context.ConnectionId}");
            _logger.LogInformation($"Установлено новое соединение {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("Notify", $"Закрыто соединение {Context.ConnectionId}");
            _logger.LogInformation($"Закрыто соединение {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Message message)
        {
            await Clients.Others.SendAsync("RecieveMessage", message);
        }
    }
}
