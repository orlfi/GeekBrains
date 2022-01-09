using System.IO;
using System.Linq;
using System.Windows;
using FileCommander.Infrastructure.EventBus;
using FileCommander.Interfaces;
using FileCommander.ViewModels.Base;
using FileCommander.ViewModels.Interfaces;
using FileCommander.ViewModels.ModelEvents;

namespace FileCommander.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        public string Title { get; set; } = "File Commander";

        public FilePanelViewModel LeftFilePanelViewModel { get; } = new FilePanelViewModel() { FilePanelName = "Left" };
        
        public FilePanelViewModel RightFilePanelViewModel { get; } = new FilePanelViewModel() { FilePanelName = "Right" };

        public string LeftPanelId => "Left";

        public string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }

        public string _fileCommand = "";
        public string FileCommand { get => _fileCommand; set => Set(ref _fileCommand, value); }

        private ModelEventBus _messageBus;
 
        private IFilePanelItem? _selectedFile;
        
        private FilePanelViewModel? _selectedFilePanelViewModel;
        
        private readonly IFileService _fileService;
        public MainWindowViewModel(IFileService fileService)
        {
            _fileService = fileService;
            _messageBus = ModelEventBus.Instance;
            LeftFilePanelViewModel = new FilePanelViewModel() { FilePanelName = "Left" };
            LeftFilePanelViewModel.FileSelectionChangeEvent += OnFileSelectionChanged;
            LeftFilePanelViewModel.CreateReportEvent += (sender, ev) => CreateReport(ev.Path);
            RightFilePanelViewModel = new FilePanelViewModel() { FilePanelName = "Right" };
            RightFilePanelViewModel.FileSelectionChangeEvent += OnFileSelectionChanged;
            RightFilePanelViewModel.CreateReportEvent += (sender, ev) => CreateReport(ev.Path);

            // _messageBus.Subscribe<FileSelectionChangeEvent>(OnSelectionChanded);
            // Files = new ObservableCollection<FileSystemInfo>(GetFileSystem(@"c:\windows"));
        }
        private void OnFileSelectionChanged(object? sender, FileSelectionChangeEventArgs? args)
        {
            _selectedFilePanelViewModel = sender as FilePanelViewModel;
            _selectedFile = args?.SelectedFile;
            FileCommand = _selectedFile?.Name ?? "" + "\t" + _selectedFilePanelViewModel?.FilePanelName + $"Выбрано {_selectedFilePanelViewModel.Files.Count(item => item.IsSelected)} файлов";
        }

        public void OnPropertyChange<T>(string propertyName, ref T value)
        {
            MessageBox.Show(value?.ToString());
        }

        private void CreateReport(string fileName)
        {
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.FileName = $"Отчет для ({Path.GetFileName(fileName)}).docx";
            dialog.DefaultExt = "docx";
            // dialog.Filter = "Microsoft word files (*.doc)|*.doc|All files (*.*)|*.*";
            dialog.Filter = "Microsoft word files (*.doc)|*.doc";
            dialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);

            if (_selectedFile is not null && dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var text = dialog.FileName;
                try
                {
                    _fileService.CreateReport(dialog.FileName, fileName);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка формирования отчета", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}