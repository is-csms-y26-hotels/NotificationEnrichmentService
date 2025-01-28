using BookingService.Application.Models.Enums;
using Google.Protobuf.WellKnownTypes;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;
using NotificationEnrichmentService.Application.Contracts.Bookings.Events;
using Notifications.Kafka.Contracts;

namespace NotificationEnrichmentService.Presentation.Kafka.ProducerHandlers.BookingNotifications;

public class BookingUpdatedNotificationHandler(
    IKafkaMessageProducer<BookingNotificationKey, BookingNotificationValue> producer)
    : IEventHandler<BookingUpdatedNotificationEvent>
{
    public async ValueTask HandleAsync(BookingUpdatedNotificationEvent evt, CancellationToken cancellationToken)
    {
        var key = new BookingNotificationKey { BookingId = evt.BookingId.Value };

        var value = new BookingNotificationValue
        {
            BookingUpdatedNotification = new BookingNotificationValue.Types.BookingUpdatedNotification
            {
                BookingId = evt.BookingId.Value,
                BookingState = MapBookingStateEnum(evt.BookingState),
                HotelName = evt.HotelName.Value,
                RoomPhysicalNumber = evt.RoomPhysicalNumber.Value,
                UserEmail = evt.BookingUserEmail.Value,
                CheckInDate = evt.BookingCheckInDate.ToTimestamp(),
                CheckOutDate = evt.BookingCheckOutDate.ToTimestamp(),
            },
        };

        var message = new KafkaProducerMessage<BookingNotificationKey, BookingNotificationValue>(key, value);
        await producer.ProduceAsync(message, cancellationToken);
    }

    private static BookingNotificationValue.Types.BookingState MapBookingStateEnum(BookingState bookingState)
    {
        return bookingState switch
        {
            BookingState.Created => BookingNotificationValue.Types.BookingState.Created,
            BookingState.Submitted => BookingNotificationValue.Types.BookingState.Submitted,
            BookingState.Cancelled => BookingNotificationValue.Types.BookingState.Cancelled,
            BookingState.Completed => BookingNotificationValue.Types.BookingState.Completed,
            _ => BookingNotificationValue.Types.BookingState.Unspecified,
        };
    }
}