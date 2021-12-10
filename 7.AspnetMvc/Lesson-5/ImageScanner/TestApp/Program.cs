using ScannerLibrary;
using ScannerLibrary.Devices;
using ScannerLibrary.Loggers;
using ScannerLibrary.Savers;

try
{
    var context = ScannerContextBuilder.Create(new ScannerDevice("scanner.txt"))
        .WithLogger(new Logger())
        .WithSaver(new ScanToPdf())
        .Build();

    context.Run("result.pdf");
    context.ConfigureProcessor(new ScanToWord());
    context.Run("result.docx");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.ReadLine();