using BookingService.Application.Models.Enums;
using Itmo.Dev.Platform.Events;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Contracts.Bookings.Events;

public record BookingUpdatedNotificationEvent(
    BookingId BookingId,
    BookingState BookingState,
    HotelName HotelName,
    RoomPhysicalNumber RoomPhysicalNumber,
    UserEmail BookingUserEmail,
    DateTimeOffset BookingCheckInDate,
    DateTimeOffset BookingCheckOutDate) : IEvent;