using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Abstractions.Gateways;

public interface IAccommodationHotelGateway
{
    public Task<HotelName> GetHotelName(HotelId hotelId, CancellationToken cancellationToken);
}