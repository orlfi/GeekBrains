using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileCommander.Commands.Base;

namespace FileCommander.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Action<object?> _execute;

        private readonly Func<object?, bool>? _canExecute;

        public LambdaCommand(Action<object?> execute, Func<object?, bool>? canExecute = default)
        {
            _execute = execute ?? throw new ArgumentException("Аргумент не может быть null", "execute");
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                || (_canExecute?.Invoke(parameter) ?? true);
        }

        public override void Execute(object? parameter) => _execute.Invoke(parameter);
    }
}