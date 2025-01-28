using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Application.Abstractions.Gateways;

public interface IAccommodationRoomGateway
{
    public Task<RoomPhysicalNumber> GetRoomPhysicalNumber(RoomId roomId, CancellationToken cancellationToken);

    public Task<HotelId> GetHotelId(RoomId roomId, CancellationToken cancellationToken);
}