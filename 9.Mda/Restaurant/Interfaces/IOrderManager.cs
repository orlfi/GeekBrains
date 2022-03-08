namespace Interfaces;

public interface IOrderManager
{
    IRestaurantBooking Restaurant { get; }
    void ProcessRequest();
}
