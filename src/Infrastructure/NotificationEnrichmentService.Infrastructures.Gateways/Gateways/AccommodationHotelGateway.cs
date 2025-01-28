using Accommodation.Service.Presentation.Grpc;
using NotificationEnrichmentService.Application.Abstractions.Gateways;
using NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

namespace NotificationEnrichmentService.Infrastructures.Gateways.Gateways;

public class AccommodationHotelGateway(
    HotelService.HotelServiceClient client) : IAccommodationHotelGateway
{
    public async Task<HotelName> GetHotelName(HotelId hotelId, CancellationToken cancellationToken)
    {
        var request = new GetHotelNameRequest { HotelId = hotelId.Value };

        GetHotelNameResponse response = await client.GetHotelNameAsync(request, cancellationToken: cancellationToken);

        return new HotelName(response.HotelName);
    }
}