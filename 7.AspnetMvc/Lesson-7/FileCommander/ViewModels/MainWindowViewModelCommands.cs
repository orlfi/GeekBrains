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
            var dialog = new System.Windows.Forms.OpenFileDialog();
            if (_selectedFile is not null && dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var text = dialog.FileName;
                System.Windows.MessageBox.Show(text);
            }
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

        #region CopyCommand
        private Command? _copyCommand;

        public ICommand CopyCommand => _copyCommand ??= Command.Invoke(OnCopyCommand).WithName("Копировать");

        public void OnCopyCommand(object? parameter)
        {
            MessageBox.Show("Copy");
        }
        #endregion

        #region MoveCommand
        private Command? _moveCommand;

        public ICommand MoveCommand => _moveCommand ??= Command.Invoke(OnMoveCommand).WithName("Копировать");

        public void OnMoveCommand(object? parameter)
        {
            MessageBox.Show("Move");
        }
        #endregion

        #region CreateDirectoryCommand
        private Command? _createDirectoryCommand;

        public ICommand CreateDirectoryCommand => _createDirectoryCommand ??= Command.Invoke(OnCreateDirectoryCommand).WithName("Копировать");

        public void OnCreateDirectoryCommand(object? parameter)
        {
            MessageBox.Show("CreateDirectory");
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