using System.Text;
using ScannerLibrary.Data;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Devices;

public class ScannerDevice : IScannerDevice
{
    public event EventHandler<MonitorChangedEventArgs>? MonitorChanged;

    private string _path;

    public ScannerDevice(string path)
    {
        _path = path;
    }

    public Stream Scan()
    {
        var ms = new MemoryStream();

        using var reader = File.OpenText(_path);
        while (reader.ReadLine() is { Length: > 0 } line)
        {
            MonitorChanged?.Invoke(this, new MonitorChangedEventArgs { Data = CollectMonitorData() });
            ms.Write(Encoding.Default.GetBytes(line));
        }
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    private static IMonitorData CollectMonitorData()
    {
        return new MonitorData
        {
            Cpu = Random.Shared.Next(0, 10) * 10,
            Memory = Random.Shared.Next(0, 10) * 10
        };
    }
}
