using Data;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;

namespace ChatClient;

public class Chat: IAsyncDisposable
{
    private readonly HubConnection _connection;
    private readonly string _login;

    public Chat(string url, string login)
    {
        _login = login;

        _connection = new HubConnectionBuilder()
            .WithAutomaticReconnect()
            .WithUrl(url)
            .Build();
        _connection.On<Message>("RecieveMessage", Recieve);
        _connection.On<string>("Notify", Notify);
        _connection.StartAsync().Wait();
    }

    public void Recieve(Message message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        var y = Console.GetCursorPosition().Top;
        Console.SetCursorPosition(0, y);
        Console.WriteLine($"{message.FromUser} -> {message.Text}");
        
        Console.SetCursorPosition(0, y+1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{_login}>");
    }

    public void Notify(string text)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        var y = Console.GetCursorPosition().Top;
        Console.SetCursorPosition(0, y);
        Console.WriteLine(text);

        Console.SetCursorPosition(0, y + 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{_login}>");
    }

    public Task SendAsync(string message)
    { 
        return _connection.InvokeAsync("SendMessage", new Message() 
        { 
            FromUser = _login, 
            ToUser = "All", 
            Text = message }
        ); 
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null)
        {
            await _connection.DisposeAsync().ConfigureAwait(false);
        }
        GC.SuppressFinalize(this);
    }
}
