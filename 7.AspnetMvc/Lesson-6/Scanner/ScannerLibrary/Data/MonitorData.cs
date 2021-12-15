using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Data;

public readonly struct MonitorData : IMonitorData
{
    public int Cpu { get; init; }
    public int Memory { get; init; }

    public override string ToString()
    {
        return $"Загрузка: CPU={Cpu}%\tMemory={Memory}";
    }
}
