using MassTransit;

namespace Restaurant.Booking;

public class RestaurantState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public int CurrentState { get; set; }
    public Guid OrderId { get; set; }

}
