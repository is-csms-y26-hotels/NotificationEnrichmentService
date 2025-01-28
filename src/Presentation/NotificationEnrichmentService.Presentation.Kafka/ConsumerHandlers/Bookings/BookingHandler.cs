using Bookings.Kafka.Contracts;
using Itmo.Dev.Platform.Kafka.Consumer;
using NotificationEnrichmentService.Application.Contracts.Bookings;
using NotificationEnrichmentService.Application.Contracts.Bookings.Models;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;
using BookingState = BookingService.Application.Models.Enums.BookingState;

namespace NotificationEnrichmentService.Presentation.Kafka.ConsumerHandlers.Bookings;

internal class BookingHandler(
    IBookingEnrichmentProcessor bookingEnrichmentProcessor) : IKafkaInboxHandler<BookingKey, BookingValue>
{
    public async ValueTask HandleAsync(
        IEnumerable<IKafkaInboxMessage<BookingKey, BookingValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaInboxMessage<BookingKey, BookingValue> message in messages)
        {
            await ProcessMessageAsync(message.Value, cancellationToken);
        }
    }

    private static BookingState MapBookingState(BookingValue.Types.BookingState bookingState)
    {
        return bookingState switch
        {
            BookingValue.Types.BookingState.Created => BookingState.Created,
            BookingValue.Types.BookingState.Submitted => BookingState.Submitted,
            BookingValue.Types.BookingState.Cancelled => BookingState.Cancelled,
            BookingValue.Types.BookingState.Completed => BookingState.Completed,
            BookingValue.Types.BookingState.Unspecified or _ =>
                throw new ArgumentOutOfRangeException(nameof(bookingState), bookingState, null),
        };
    }

    private async Task ProcessMessageAsync(BookingValue booking, CancellationToken cancellationToken)
    {
        switch (booking.EventCase)
        {
            case BookingValue.EventOneofCase.BookingCreated:
                await ProcessBookingCreatedEventAsync(booking.BookingCreated, cancellationToken);
                break;
            case BookingValue.EventOneofCase.BookingUpdated:
                await ProcessBookingUpdatedEventAsync(booking.BookingUpdated, cancellationToken);
                break;
            case BookingValue.EventOneofCase.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(booking));
        }
    }

    private async Task ProcessBookingCreatedEventAsync(
        BookingValue.Types.BookingCreated evt,
        CancellationToken cancellationToken)
    {
        var model = new ProcessCreatedBookingModel(
            new BookingId(evt.BookingId),
            new UserEmail(evt.UserEmail),
            new RoomId(evt.RoomId),
            evt.CheckInDate.ToDateTimeOffset(),
            evt.CheckOutDate.ToDateTimeOffset(),
            evt.CreatedAt.ToDateTimeOffset());

        await bookingEnrichmentProcessor.ProcessCreatedBooking(model, cancellationToken);
    }

    private async Task ProcessBookingUpdatedEventAsync(
        BookingValue.Types.BookingUpdated evt,
        CancellationToken cancellationToken)
    {
        var model = new ProcessUpdatedBookingModel(
            new BookingId(evt.BookingId),
            MapBookingState(evt.BookingState),
            new UserEmail(evt.UserEmail),
            new RoomId(evt.RoomId),
            evt.CheckInDate.ToDateTimeOffset(),
            evt.CheckOutDate.ToDateTimeOffset());

        await bookingEnrichmentProcessor.ProcessUpdatedBooking(model, cancellationToken);
    }
}