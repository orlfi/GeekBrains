using ChatClient;

Console.Write("Login: ");
var login = Console.ReadLine();

await using var client = new Chat(" http://localhost:5208/chat", login);

while (true)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"{login}>");
    var message = Console.ReadLine();
    await client.SendAsync(message);
}