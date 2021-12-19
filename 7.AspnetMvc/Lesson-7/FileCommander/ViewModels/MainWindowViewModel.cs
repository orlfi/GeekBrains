using System.Windows;
using System.Windows.Input;
using FileCommander.Commands.Base;
using FileCommander.ViewModels.Base;

namespace FileCommander.ViewModels
{
    public partial  class MainWindowViewModel : ViewModel
    {
        public string Title { get; set; } = "File Commander";

        public string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }
    }
}