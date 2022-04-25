using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Restaurant.Booking.Consumers;
using Restaurant.Booking.DTO;
using Restaurant.Booking.Interfaces;
using Restaurant.Booking.Models;
using Restaurant.Booking.Services;
using Restaurant.Messaging.Data;
using Restaurant.Messaging.Interfaces;
using Restaurant.Messaging.Repositories;

namespace Restaurant.Tests;

public class ConsumerTests
{
    private ServiceProvider _provider;
    private InMemoryTestHarness _harness;

    [OneTimeSetUp]
    public async Task Init()
    {
        _provider = new ServiceCollection()
            .AddMassTransitInMemoryTestHarness(cfg =>
            {
                cfg.AddConsumer<BookingRequestedConsumer>();
                cfg.AddConsumerTestHarness<BookingRequestedConsumer>();
            })
            .AddLogging()
            .AddSingleton<IInMemoryRepository<BookingRequestModel>, InMemoryRepository<BookingRequestModel>>()
            .AddSingleton<ITableBookingService, TableBookingService>()
            .BuildServiceProvider();

        _harness = _provider.GetRequiredService<InMemoryTestHarness>();

        await _harness.Start();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await _harness.Stop();
        await _provider.DisposeAsync();
    }

    [Test]
    public async Task Any_Booking_Request_Consumed()
    {
        await _harness.Bus.Publish(new BookingRequested()
        {
            OrderId = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Dish = MenuRepository.GetDishById(1),
            Seats = 5,
            BookingArrivalTime = 7,
            ActualArrivalTime = 2
        } as IBookingRequested);

        Assert.That(await _harness.Consumed.Any<IBookingRequested>());
        await _harness.OutputTimeline(TestContext.Out, opt => opt.Now().IncludeAddress());
    }
}