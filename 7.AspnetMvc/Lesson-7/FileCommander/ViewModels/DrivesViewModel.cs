using FileCommander.ViewModels.Base;
using FileCommander.ViewModels.ModelEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCommander.ViewModels;

public class DrivesViewModel : ViewModel
{
    public event EventHandler<FileChangeEventArgs> DriveSelectionChangeEvent;

    private DriveInfo _selectedDrive;

    public DriveInfo SelectedDrive
    {
        get => _selectedDrive;
        set
        {
            Set(ref _selectedDrive, value);
            DriveSelectionChangeEvent?.Invoke(this, new FileChangeEventArgs(value.Name));
        }
    }

    public ObservableCollection<DriveInfo> Drives { get; set; } = new ObservableCollection<DriveInfo>();

    public DrivesViewModel()
    {
        ReadDrives();
        
        if (Drives.Count >0 )
            SelectedDrive = Drives[0];
    }

    private void ReadDrives()
    {
        Drives.Clear();
        var drives = DriveInfo.GetDrives();
        foreach (var drive in drives)
        {
            Drives.Add(drive);
        }
    }

}
