using System.Windows.Input;

namespace Data.Commands;
public record Command : ICommand
{
    internal Action<object?>? ExecuteHandle { get; init; }

    internal Func<object?, bool>? CanExecuteHandle { get; init; } 

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public event EventHandler? CanExecuteChanged;

    public static CommandBuilder Invoke(Action<object?> executeHandle) => 
        new CommandBuilder(executeHandle, default, "", "");

    internal Command() { }

    public bool CanExecute(object? parameter) => CanExecuteHandle != null ? CanExecuteHandle(parameter) : true;

    public void Execute(object? parameter)
    {
        if (ExecuteHandle is not null)
            ExecuteHandle(parameter);
    }
}

public record struct CommandBuilder(Action<object?>? ExecuteHandle, Func<object?, bool>? CanExecute, string Name, string Description)
{
    public static implicit operator Command(CommandBuilder builder) => new()
    {
        ExecuteHandle = builder.ExecuteHandle,
        CanExecuteHandle = builder.CanExecute,
        Name = builder.Name,
        Description = builder.Description
    };
}
