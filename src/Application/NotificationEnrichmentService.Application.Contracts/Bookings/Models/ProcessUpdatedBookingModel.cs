using BookingService.Application.Models.Enums;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Contracts.Bookings.Models;

public record ProcessUpdatedBookingModel(
    BookingId BookingId,
    BookingState BookingState,
    UserEmail UserEmail,
    RoomId RoomId,
    DateTimeOffset CheckInDate,
    DateTimeOffset CheckOutDate);