using Accommodation.Service.Presentation.Grpc;
using NotificationEnrichmentService.Application.Abstractions.Gateways;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

public class AccommodationRoomGateway(
    RoomService.RoomServiceClient client) : IAccommodationRoomGateway
{
    public async Task<RoomPhysicalNumber> GetRoomPhysicalNumber(RoomId roomId, CancellationToken cancellationToken)
    {
        var request = new GetRoomPhysicalNumberRequest
        {
            RoomId = roomId.Value,
        };

        GetRoomPhysicalNumberResponse response = await client.GetRoomPhysicalNumberAsync(
            request,
            cancellationToken: cancellationToken);

        return new RoomPhysicalNumber(response.RoomNumber);
    }

    public async Task<HotelId> GetHotelId(RoomId roomId, CancellationToken cancellationToken)
    {
        var request = new GetHotelIdByRoomIdRequest
        {
            RoomId = roomId.Value,
        };

        GetHotelIdByRoomIdResponse response = await client.GetHotelIdByRoomIdAsync(
            request,
            cancellationToken: cancellationToken);

        return new HotelId(response.HotelId);
    }
}