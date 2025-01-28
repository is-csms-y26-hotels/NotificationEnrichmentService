using Itmo.Dev.Platform.Events;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Contracts.Bookings.Events;

public record BookingCreatedNotificationEvent(
    BookingId BookingId,
    HotelName HotelName,
    RoomPhysicalNumber RoomPhysicalNumber,
    UserEmail BookingUserEmail,
    DateTimeOffset BookingCheckInDate,
    DateTimeOffset BookingCheckOutDate,
    DateTimeOffset BookingCreatedAt) : IEvent;