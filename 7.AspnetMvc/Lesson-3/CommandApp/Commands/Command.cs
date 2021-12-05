using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandApp.Commands
{
    public record  Command : ICommand
    {
        internal Action<object> ExecuteHandle { get; init; }
        
        internal Func<object, bool> CanExecuteHandle { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        
        public event EventHandler? CanExecuteChanged;

        internal Command() { }

        public static CommandBuilder Invoke(Action<object> executeHandle)
        {
            return new CommandBuilder(executeHandle, default, "", "");
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteHandle(parameter);
        }

        public void Execute(object? parameter)
        {
            ExecuteHandle(parameter);
        }
    }

    public record CommandBuilder(Action<object> ExecuteHandle, Func<object, bool> CanExecute, string Name, string Description)
    {
        public static implicit operator Command(CommandBuilder builder) => new Command 
        { 
            ExecuteHandle = builder.ExecuteHandle, 
            CanExecuteHandle = builder.CanExecute, 
            Name = builder.Name,
            Description = builder.Description
        };
    }

    class MyClass
    {
        private Command? _command;

        public ICommand Command => _command ?? Commands.Command.Invoke(this.CommandHandler) with 
        { 
            CanExecute = CanExecute,
            Name = "Тестовая команда",
            Description = "Описание команды"
            
        };

        public void CommandHandler(object param)
        {
            Debug.WriteLine("Execute");
        }
        public bool CanExecute(object param)
        {
            return true;
        }

    }
}
