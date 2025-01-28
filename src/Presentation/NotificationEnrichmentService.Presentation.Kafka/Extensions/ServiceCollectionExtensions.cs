using Bookings.Kafka.Contracts;
using Itmo.Dev.Platform.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationEnrichmentService.Presentation.Kafka.ConsumerHandlers.Bookings;
using Notifications.Kafka.Contracts;

namespace NotificationEnrichmentService.Presentation.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationKafka(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        const string consumerKey = "Presentation:Kafka:Consumers";
        const string producerKey = "Presentation:Kafka:Producers";

        collection.AddPlatformKafka(builder => builder
            .ConfigureOptions(configuration.GetSection("Presentation:Kafka"))
            .AddConsumer(b => b
                .WithKey<BookingKey>()
                .WithValue<BookingValue>()
                .WithConfiguration(configuration.GetSection($"{consumerKey}:Bookings"))
                .DeserializeKeyWithProto()
                .DeserializeValueWithProto()
                .HandleInboxWith<BookingHandler>())
            .AddProducer(b => b
                .WithKey<BookingNotificationKey>()
                .WithValue<BookingNotificationValue>()
                .WithConfiguration(configuration.GetSection($"{producerKey}:BookingNotifications"))
                .SerializeKeyWithProto()
                .SerializeValueWithProto()
                .WithOutbox()));

        return collection;
    }
}