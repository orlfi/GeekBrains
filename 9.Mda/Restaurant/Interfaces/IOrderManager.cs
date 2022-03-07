namespace Interfaces;

public interface IOrderManager
{
    IRestaurant Restaurant { get; }
    void ProcessRequest();
}
