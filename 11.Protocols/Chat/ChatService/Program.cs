using ChatServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.Run();
