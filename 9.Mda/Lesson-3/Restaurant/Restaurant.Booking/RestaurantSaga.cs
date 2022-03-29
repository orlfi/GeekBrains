
using MassTransit;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking;

public class RestaurantSaga : MassTransitStateMachine<RestaurantState>
{
    public State AwaitingBookingApproved { get; private set; }
    public Event<ITableBooked> TableBooked { get; private set; }
    public RestaurantSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => TableBooked, x =>
            x.CorrelateById(context => context.Message.OrderId)
                .SelectId(context => context.Message.OrderId));

        Initially(
            When(TableBooked)
                .Then(context =>
                {
                    Console.WriteLine("[Saga] Бронирование столика для клиента {0}", context.Message.ClientId);
                })
         );
    }
}
