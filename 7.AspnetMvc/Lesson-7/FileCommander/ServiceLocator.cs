using FileCommander.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FileCommander;

public class ServiceLocator
{
    public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
}
