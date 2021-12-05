using Data.Commands;
using Data.Interfaces;
using Data.Repositories;
using Data.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandApp;
public class MainWindowViewModel : ViewModel
{
    private readonly IWeatherForecastRepository _db;

    private string _title = "Прогноз погоды";
    
    public string Title { get => _title; set => Set(ref _title, value); }

    private string _currentTime;
    
    public string CurrentTime { get => _currentTime; set => Set(ref _currentTime, value); }

    private DateTime _date;
    public DateTime Date { get => _date; set => Set(ref _date, value); }

    private int _temperature;
    public int Temperature { get => _temperature; set => Set(ref _temperature, value); }

    private string _summary;
    public string Summary { get => _summary; set => Set(ref _summary, value); }

    private Command? _refreshCommand;

    public ICommand  RefreshCommand => _refreshCommand??= Command.Invoke(RefreshCommandHandler) with { CanExecute = CanExecuteRefreshCommand };

    public MainWindowViewModel()
    {
        _db = new WeatherForecastRepository();
        RefreshData();
        UpdateTime();
    }

    private async Task UpdateTime()
    {
        while (true)
        {
            CurrentTime = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.ff");
            await Task.Delay(100);
        }

    }
    private void RefreshData() => (Date, Temperature, Summary) = _db.GetByDay(Random.Shared.Next(1, 5));

    private void RefreshCommandHandler(object obj) => RefreshData();

    private bool CanExecuteRefreshCommand(object obj) => true;
}

