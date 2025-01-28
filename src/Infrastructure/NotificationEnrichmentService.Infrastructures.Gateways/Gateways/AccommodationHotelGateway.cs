using NotificationEnrichmentService.Application.Abstractions.Gateways;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

public class AccommodationHotelGateway : IAccommodationHotelGateway
{
    public Task<HotelName> GetHotelName(HotelId hotelId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HotelName("hotel_name-1"));
    }
}