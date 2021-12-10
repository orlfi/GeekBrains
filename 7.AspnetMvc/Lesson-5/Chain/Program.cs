
using Chain;

var notebookData = new DeviceMonitorData
{
    Name = "Notebook",
    Cpu = 50,
    Memory = 70
};

var desktopData = new DeviceMonitorData
{
    Name = "Desktop",
    Cpu = 30,
    Memory = 0
};

var notebookMonitorPipeline = new MonitorDevicePipeline(notebookData);
notebookMonitorPipeline.RunPipeline();


var desktopMonitorContect = new MonitorDevicePipeline(desktopData);
desktopMonitorContect.RunPipeline();

Console.ReadLine();
