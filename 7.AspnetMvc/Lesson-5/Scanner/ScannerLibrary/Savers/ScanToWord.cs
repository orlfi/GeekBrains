using ScannerLibrary.Interfaces;
using ScannerLibrary.Savers.Base;

namespace ScannerLibrary.Savers;

public class ScanToWord : ScanSaver
{
    public override void ScanAndSave(IScannerDevice device, string fileName)
    {
        if (Path.GetExtension(fileName).ToLower() != ".docx")
            throw new IOException("Ошибка формата файла");

        using var writer = new FileStream(fileName, FileMode.Create);
        var reader = device.Scan();
        var buffer = new byte[1024];
        int read = 0;
        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
        {
            writer.Write(buffer, 0, read);
        }
    }
}
