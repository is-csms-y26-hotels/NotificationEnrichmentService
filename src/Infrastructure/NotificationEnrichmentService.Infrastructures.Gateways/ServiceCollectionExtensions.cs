using Itmo.Dev.Platform.Grpc.Clients;
using Microsoft.Extensions.DependencyInjection;
using NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

namespace NotificationEnrichmentService.Infrastructures.Gateways;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureGateways(this IServiceCollection collection)
    {
        collection.AddPlatformGrpcClients(clients => clients
            .AddAccommodationGatewayClient());

        collection.AddAccommodationGateway();

        return collection;
    }
}