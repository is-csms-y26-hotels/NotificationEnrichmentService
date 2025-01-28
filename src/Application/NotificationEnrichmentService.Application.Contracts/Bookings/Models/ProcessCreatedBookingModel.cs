using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Contracts.Bookings.Models;

public record ProcessCreatedBookingModel(
    BookingId BookingId,
    UserEmail UserEmail,
    RoomId RoomId,
    DateTimeOffset CheckInDate,
    DateTimeOffset CheckOutDate,
    DateTimeOffset CreatedAt);