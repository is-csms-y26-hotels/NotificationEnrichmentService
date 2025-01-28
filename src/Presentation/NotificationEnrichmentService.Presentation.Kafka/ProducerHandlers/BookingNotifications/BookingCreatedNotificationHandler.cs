using Google.Protobuf.WellKnownTypes;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;
using NotificationEnrichmentService.Application.Contracts.Bookings.Events;
using Notifications.Kafka.Contracts;

namespace NotificationEnrichmentService.Presentation.Kafka.ProducerHandlers.BookingNotifications;

public class BookingCreatedNotificationHandler(
    IKafkaMessageProducer<BookingNotificationKey, BookingNotificationValue> producer)
    : IEventHandler<BookingCreatedNotificationEvent>
{
    public async ValueTask HandleAsync(BookingCreatedNotificationEvent evt, CancellationToken cancellationToken)
    {
        var key = new BookingNotificationKey { BookingId = evt.BookingId.Value };

        var value = new BookingNotificationValue
        {
            BookingCreatedNotification = new BookingNotificationValue.Types.BookingCreatedNotification
            {
                BookingId = evt.BookingId.Value,
                HotelName = evt.HotelName.Value,
                RoomPhysicalNumber = evt.RoomPhysicalNumber.Value,
                UserEmail = evt.BookingUserEmail.Value,
                CheckInDate = evt.BookingCheckInDate.ToTimestamp(),
                CheckOutDate = evt.BookingCheckOutDate.ToTimestamp(),
                CreatedAt = evt.BookingCreatedAt.ToTimestamp(),
            },
        };

        var message = new KafkaProducerMessage<BookingNotificationKey, BookingNotificationValue>(key, value);
        await producer.ProduceAsync(message, cancellationToken);
    }
}