using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FileCommander.Commands.Base;

namespace FileCommander.Commands
{
    public class DebugCommand : Command, ICommand
    {
        private readonly ICommand _baseCommand;

        public DebugCommand(ICommand baseCommand) => _baseCommand = baseCommand;

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Debug.WriteLine("Вызов команды {0} с параметром {1}", _baseCommand.GetType(), parameter);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _baseCommand.Execute(parameter);
            sw.Stop();
            Debug.WriteLine("Вызов команды {0} с параметром {1} завершен за {2} мс", _baseCommand.GetType(), parameter, sw.ElapsedMilliseconds);
        }
    }
}