using System.IO;
using System.Windows;
using System.Windows.Input;
using FileCommander.Commands.Base;
using FileCommander.ViewModels.Base;

namespace FileCommander.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        #region ReportCommand
        private Command? _reportCommand;

        public ICommand ReportCommand => _reportCommand ??= Command.Invoke(OnReportCommand).WithName("Отчет");

        public void OnReportCommand(object? parameter)
        {
            if (_selectedFile is not null)
                MessageBox.Show(_selectedFile.Name);
        }
        #endregion



        #region QuitCommand
        private Command? _quitCommand;

        public ICommand QuitCommand => _quitCommand ??= Command.Invoke(OnQuitCommand).WithName("Выход");

        public void OnQuitCommand(object? parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region TestCommand
        private Command? _testCommand;

        public ICommand TestCommand => _testCommand ??= Command.Invoke(OnTestCommand).WithName("Нажми меня");

        public void OnTestCommand(object? parameter)
        {
            MessageBox.Show("test");
        }
        #endregion
    }
}