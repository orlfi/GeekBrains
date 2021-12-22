using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileCommander.Commands.Base
{
    public abstract class Command: ICommand
    {
        public string Name { get; set; } = "Button";

        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => true;

        public abstract void Execute(object? parameter);

        public static CommandBuilder Invoke(Action<object?> execute)
        {
            return new CommandBuilder(execute);
        }
    }
}