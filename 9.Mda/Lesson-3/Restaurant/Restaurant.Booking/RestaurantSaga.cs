
using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Booking;

public class RestaurantSaga : MassTransitStateMachine<RestaurantState>
{
    private readonly ILogger<RestaurantSaga> _logger;

    public State AwaitingBookingApproved { get; private set; }
    public State AwaitingGuestArrival { get; private set; }

    public Event<IBookingRequested> BookingRequestedEvent { get; private set; }
    public Event<ITableBooked> TableBookedEvent { get; private set; }
    public Event<IKitchenReady> KitchenReadyEvent { get; private set; }
    public Event<IKitchenReject> KitchenRejectEvent { get; private set; }
    public Event BookingApprovedEvent { get; private set; }

    public Schedule<RestaurantState, IBookingExpired> BookingExpiredSchedule { get; private set; }

    // Ожидание гостя
    public Schedule<RestaurantState, IGuestArrived> AwaitingGuestSchedule { get; private set; }

    //прибытие гостя
    public Schedule<RestaurantState, IGuestArrived> GuestArrivalSchedule { get; private set; }

    public RestaurantSaga(ILogger<RestaurantSaga> logger)
    {
        _logger = logger;

        InstanceState(x => x.CurrentState);

        Event(() => BookingRequestedEvent, x =>
            x.CorrelateById(context => context.Message.OrderId)
            .SelectId(context => context.Message.OrderId));

        Event(() => TableBookedEvent, x =>
            x.CorrelateById(context => context.Message.OrderId));

        Event(() => KitchenRejectEvent, x =>
            x.CorrelateById(context => context.Message.OrderId));

        Event(() => KitchenReadyEvent, x =>
            x.CorrelateById(context => context.Message.OrderId));

        CompositeEvent(() => BookingApprovedEvent,
            x => x.ReadyEventStatus, TableBookedEvent, KitchenReadyEvent);

        Schedule(() => BookingExpiredSchedule,
            state => state.BookingExpirationId,
            config =>
            {
                config.Delay = TimeSpan.FromSeconds(3);
                config.Received = e => e.CorrelateById(context => context.Message.OrderId);
            }
        );

        // Schedule(() => AwaitingGuestSchedule,
        //     state => state.GuestAwaitingId,
        //     config =>
        //     {
        //         Random rnd = new Random();
        //         config.Delay = TimeSpan.FromSeconds(rnd.Next(7, 16));
        //         config.Received = e => e.CorrelateById(context => context.Message.OrderId);
        //     }
        // );

        Schedule(() => GuestArrivalSchedule,
            state => state.GuestArrivalId,
            config =>
            {
                Random rnd = new Random();
                config.Delay = TimeSpan.FromSeconds(rnd.Next(7, 16));
                config.Received = e => e.CorrelateById(context => context.Message.OrderId);
            }
        );

        Initially(
            When(BookingRequestedEvent)
                .Then(context =>
                {
                    context.Saga.CorrelationId = context.Message.OrderId;
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.ClientId = context.Message.ClientId;
                    context.Saga.ArrivalTime = context.Message.ArrivalTime;
                    _logger.LogInformation("[Saga] Запрос на бронирование столика для клиента {ClientId} на {Seats} мест", context.Message.ClientId, context.Message.Seats);
                })
                .Schedule(BookingExpiredSchedule, context => new BookingExpired(context.Saga))
                .TransitionTo(AwaitingBookingApproved)
         );

        During(AwaitingBookingApproved,
            When(BookingApprovedEvent)
                .Unschedule(BookingExpiredSchedule)
                .Publish(context =>
                    new Notify()
                    {
                        OrderId = context.Saga.OrderId,
                        ClientId = context.Saga.ClientId,
                        Message = "Стол успешно забронирован"
                    })
                .Schedule(GuestArrivalSchedule, context =>
                {
                    _logger.LogInformation("[Saga] Ожидание гостя...");
                    return new GuestArrived(context.Saga);
                })
                // .Schedule(AwaitingGuestSchedule, context => context.Init<IGuestWaitingExpired>(new { OrderId = context.Instance.CorrelationId }),
                //     context => TimeSpan.FromSeconds(5))
                .TransitionTo(AwaitingGuestArrival),

            When(KitchenRejectEvent)
                .Unschedule(BookingExpiredSchedule)
                .Then(content => _logger.LogInformation("[Saga] Отмена кухни для заказа {OrderId}", content.Message.OrderId))
                .Publish(context =>
                    new BookingCancel()
                    {
                        OrderId = context.Saga.OrderId
                    })
                .Finalize(),

            When(BookingExpiredSchedule.Received)
                .Then(context => _logger.LogInformation("[Saga] Отмена заказа {OrderId} по времени BookingExpiredSchedule", context.Message.OrderId))
                .Finalize()
        );

        During(AwaitingGuestArrival,
            When(GuestArrivalSchedule.Received)
                .Then(context =>
                {
                    _logger.LogInformation("[Saga] Гость прибыл!");
                })
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
