using TestCommand;
using Data.Commands;
using System.Windows.Input;

Command simpleCommand = Command.Invoke((obj) => Console.WriteLine("Выполнение простой команды")) ;

simpleCommand.Execute(default);

var viewModel = new TestViewModel();

viewModel.TestCommand.Execute("test");

Console.ReadLine();
