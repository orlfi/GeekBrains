namespace ReportSender.Domain;

public sealed class Message
{
    public string Subject { get; init; } = null!;

    public string Body { get; init; } = null!;

    public string To { get; init; } = null!;

    public string Name { get; init; } = null!;

    public bool IsHtml { get; init; } = false;
}
