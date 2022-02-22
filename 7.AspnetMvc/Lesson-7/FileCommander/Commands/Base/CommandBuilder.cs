using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileCommander.Commands.Base
{
    public class CommandBuilder
    {
        private string _name = "Button";

        private Action<object?>? _execute;

        private Func<object?, bool>? _canExecute;

        private bool _debug;

        public CommandBuilder() { }

        public CommandBuilder(Action<object?> execute) => _execute = execute;

        public static CommandBuilder Create(Action<object?> execute)
        {
            return new CommandBuilder(execute);
        }

        public static implicit operator Command(CommandBuilder commandBuilder)
        {
            return (Command)commandBuilder.Build();
        }

        public CommandBuilder Invoke(Action<object?> execute)
        {
            _execute += execute;
            return this;
        }
        public CommandBuilder When(Func<object?, bool> canExecute)
        {
            _canExecute = canExecute;
            return this;
        }

        public CommandBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CommandBuilder Debug(bool debug)
        {
            _debug = debug;
            return this;
        }

        public ICommand Build()
        {
            var command = new LambdaCommand(_execute!, _canExecute) { Name = _name };
            return !_debug ? command
                : new DebugCommand(command);
        }
    }
}