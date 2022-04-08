﻿
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

    // Отслеживает время прибытия указанное гостем при бронировании
    public Schedule<RestaurantState, IGuestWaitingExpired> BookingAwaitingGuestSchedule { get; private set; }

    // Отслеживает фактическое время прибытия гостя
    public Schedule<RestaurantState, IGuestArrived> ActualGuestArrivalSchedule { get; private set; }

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

        Schedule(() => ActualGuestArrivalSchedule,
            state => state.GuestArrivalId,
            config =>
            {
                config.Delay = TimeSpan.FromSeconds(1);
                config.Received = e => e.CorrelateById(context => context.Message.OrderId);
            }
        );

        Schedule(() => BookingAwaitingGuestSchedule,
            state => state.GuestAwaitingId,
            config =>
            {
                config.Delay = TimeSpan.FromSeconds(1);
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
                    context.Saga.BookingArrivalTime = context.Message.BookingArrivalTime;
                    context.Saga.ActualArrivalTime = context.Message.ActualArrivalTime;
                    _logger.LogInformation("[Saga] Запрос на бронирование столика для клиента {ClientId} на {Seats} мест", context.Message.ClientId, context.Message.Seats);
                })
                .Schedule(BookingExpiredSchedule, context => new BookingExpired(context.Saga))
                .TransitionTo(AwaitingBookingApproved)
         );

        During(AwaitingBookingApproved,
            When(BookingApprovedEvent)
                .Unschedule(BookingExpiredSchedule)
                .Publish(context => (INotify)
                    new Notify()
                    {
                        OrderId = context.Saga.OrderId,
                        ClientId = context.Saga.ClientId,
                        Message = "Стол успешно забронирован"
                    })
                .Schedule(ActualGuestArrivalSchedule, context =>
                    {
                        _logger.LogInformation("[Saga] Фактическое ожидание прибытия гостя...");
                        return new GuestArrived(context.Saga);
                    },
                    context => TimeSpan.FromSeconds(context.Saga.ActualArrivalTime)
                )
                .Schedule(BookingAwaitingGuestSchedule, context =>
                    {
                        _logger.LogInformation("[Saga] Ожидание гостя ко времени бронирования...");
                        return new GuestWaitingExpired(context.Saga);
                    },
                    context => TimeSpan.FromSeconds(context.Saga.BookingArrivalTime)
                )
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

            When(BookingExpiredSchedule?.Received)
                .Then(context => _logger.LogInformation("[Saga] Отмена заказа {OrderId} по времени BookingExpiredSchedule", context.Message.OrderId))
                .Finalize()
        );

        During(AwaitingGuestArrival,
            When(ActualGuestArrivalSchedule?.Received)
                .Unschedule(BookingAwaitingGuestSchedule)
                .Then(context =>
                {
                    _logger.LogInformation("[Saga] Гость прибыл в течении времени бронирования!");
                })
                .Publish(context => (INotify)
                    new Notify()
                    {
                        OrderId = context.Saga.OrderId,
                        ClientId = context.Saga.ClientId,
                        Message = "Просим пройти за столик."
                    })
                .Finalize(),

            When(BookingAwaitingGuestSchedule?.Received)
                .Unschedule(ActualGuestArrivalSchedule)
                .Then(context =>
                {
                    _logger.LogInformation("[Saga] Время бронирования вышло, гость не прибыл!");
                })
                .Publish(context =>
                    new BookingCancel()
                    {
                        OrderId = context.Saga.OrderId
                    })
                .Publish(context => (INotify)
                    new Notify()
                    {
                        OrderId = context.Saga.OrderId,
                        ClientId = context.Saga.ClientId,
                        Message = "Вы не пришли в указанное время, бронь снята."
                    })
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
