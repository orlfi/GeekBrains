using System.IO;
using System.Text;
using ScannerLibrary.Data;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Devices;

public class FileScannerDevice : IScannerDevice
{
    public event EventHandler<NewScanEventArgs>? NewScanEvent;

    private readonly FileSystemWatcher _watcher;

    private readonly string _path;

    public FileScannerDevice(string path)
    {
        _path = path;
        _watcher = new FileSystemWatcher(path);
        _watcher.Filter = "*.bmp";
        _watcher.IncludeSubdirectories = false;
    }

    public void Scan()
    {
        _watcher.Created += async (sender, e) =>
        {
            var data = await ReadFile(e.Name!);
            var picture = new Picture() { Data = data };
            NewScanEvent?.Invoke(this, new NewScanEventArgs() { FileName = Path.GetFileName(e.Name), Picture = picture });
        };
        _watcher.EnableRaisingEvents = true;
    }

    private async Task<byte[]> ReadFile(string fileName)
    {
        await Task.Run(() =>
        {
            bool fileLocked = true;
            while (fileLocked)
            {
                try
                {
                    using var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    fileLocked = false;
                }
                catch (IOException) { }
            }
        });
        return await File.ReadAllBytesAsync(fileName);
    }
}
