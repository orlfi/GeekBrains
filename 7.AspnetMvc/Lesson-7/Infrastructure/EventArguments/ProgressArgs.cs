namespace FileCommander.Infrastructure.EventArguments;

public class ProgressArgs : EventArgs
{
    public int Progress { get; }

    public ProgressArgs(int progress) => Progress = progress;
}
