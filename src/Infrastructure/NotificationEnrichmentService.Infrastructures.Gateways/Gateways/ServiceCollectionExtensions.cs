using Microsoft.Extensions.DependencyInjection;
using NotificationEnrichmentService.Application.Abstractions.Gateways;

namespace NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccommodationGateway(this IServiceCollection collection)
    {
        collection.AddScoped<IAccommodationHotelGateway, AccommodationHotelGateway>();
        collection.AddScoped<IAccommodationRoomGateway, AccommodationRoomGateway>();

        return collection;
    }
}