namespace FileCommander.ViewModels.Interfaces;

public interface IDriveItem
{
    string Name { get; set; }
    string Path { get; init; }
}
