using Interfaces;
using Services.ChoiceStates;

namespace Services.Request;

public class InteractiveOrderManager : IInteractiveOrderManager
{
    private readonly IRestaurant _restaurant;

    public IRestaurant Restaurant => _restaurant;
    public IMenu? CurrentMenu { get; set; }

    public InteractiveOrderManager(IRestaurant restaurant)
    {
        _restaurant = restaurant;
    }

    public void ProcessRequest()
    {
        CurrentMenu = new MainMenu();
        while (CurrentMenu.GetChoice(this)) ;
    }
}
