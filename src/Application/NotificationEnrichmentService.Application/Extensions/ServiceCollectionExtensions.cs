using Microsoft.Extensions.DependencyInjection;
using NotificationEnrichmentService.Application.Contracts.Bookings;
using NotificationEnrichmentService.Application.Processors;

namespace NotificationEnrichmentService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IBookingEnrichmentProcessor, BookingEnrichmentService>();

        return collection;
    }
}