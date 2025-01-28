using NotificationEnrichmentService.Application.Abstractions.Gateways;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

public class AccommodationRoomGateway : IAccommodationRoomGateway
{
    public Task<RoomPhysicalNumber> GetRoomPhysicalNumber(RoomId roomId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RoomPhysicalNumber(125));
    }

    public Task<HotelId> GetHotelId(RoomId roomId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HotelId(555));
    }
}