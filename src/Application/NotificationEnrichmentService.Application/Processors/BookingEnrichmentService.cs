using Itmo.Dev.Platform.Events;
using NotificationEnrichmentService.Application.Abstractions.Gateways;
using NotificationEnrichmentService.Application.Contracts.Bookings;
using NotificationEnrichmentService.Application.Contracts.Bookings.Events;
using NotificationEnrichmentService.Application.Contracts.Bookings.Models;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Processors;

public class BookingEnrichmentService(
    IAccommodationHotelGateway accommodationHotelGateway,
    IAccommodationRoomGateway accommodationRoomGateway,
    IEventPublisher eventPublisher) : IBookingEnrichmentProcessor
{
    public async Task ProcessCreatedBooking(ProcessCreatedBookingModel model, CancellationToken cancellationToken)
    {
        HotelId hotelId = await accommodationRoomGateway.GetHotelId(model.RoomId, cancellationToken);
        HotelName hotelName = await accommodationHotelGateway.GetHotelName(hotelId, cancellationToken);

        RoomPhysicalNumber roomPhysicalNumber = await accommodationRoomGateway.GetRoomPhysicalNumber(model.RoomId, cancellationToken);

        var evt = new BookingCreatedNotificationEvent(
            model.BookingId,
            hotelName,
            roomPhysicalNumber,
            model.UserEmail,
            model.CheckInDate,
            model.CheckOutDate,
            model.CreatedAt);

        await eventPublisher.PublishAsync(evt, cancellationToken);
    }

    public async Task ProcessUpdatedBooking(ProcessUpdatedBookingModel model, CancellationToken cancellationToken)
    {
        HotelId hotelId = await accommodationRoomGateway.GetHotelId(model.RoomId, cancellationToken);
        HotelName hotelName = await accommodationHotelGateway.GetHotelName(hotelId, cancellationToken);

        RoomPhysicalNumber roomPhysicalNumber = await accommodationRoomGateway.GetRoomPhysicalNumber(model.RoomId, cancellationToken);

        var evt = new BookingUpdatedNotificationEvent(
            model.BookingId,
            model.BookingState,
            hotelName,
            roomPhysicalNumber,
            model.UserEmail,
            model.CheckInDate,
            model.CheckOutDate);

        await eventPublisher.PublishAsync(evt, cancellationToken);
    }
}