namespace Interfaces;

public interface IInteractiveOrderManager: IOrderManager
{
    IMenu CurrentMenu{ get; set; }
}
