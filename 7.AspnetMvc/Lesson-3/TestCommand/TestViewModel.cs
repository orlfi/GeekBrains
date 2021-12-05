using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Commands;
using System.Windows.Input;

namespace TestCommand
{
    class TestViewModel
    {
        private Command? _testCommand;

        private string _info = "Выполнение команды из TestViewModel";

        public ICommand TestCommand => _testCommand ??= Command.Invoke(CommandHandler) with
        {
            CanExecute = CanExecute,
            Name = "Тестовая команда",
            Description = "Описание команды"
        };

        public void CommandHandler(object param)
        {
            Console.WriteLine($"{_info} с параметром {param}");
        }
        public bool CanExecute(object param)
        {
            return param is not null;
        }

    }
}
