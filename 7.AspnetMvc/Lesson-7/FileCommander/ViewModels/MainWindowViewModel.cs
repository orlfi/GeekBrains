using System.Windows;
using System.Windows.Input;
using FileCommander.Commands.Base;
using FileCommander.ViewModels.Base;

namespace FileCommander.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public string Title { get; set; } = "Привет из VSCode";

        public string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }

        private Command? _testCommand;

        public ICommand TestCommand => _testCommand ??= Command.Invoke(OnTestCommand).WithName("Нажми меня");

        public void OnTestCommand(object? parameter)
        {
            MessageBox.Show("test");
        }
    }
}