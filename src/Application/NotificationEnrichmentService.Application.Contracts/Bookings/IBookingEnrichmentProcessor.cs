using NotificationEnrichmentService.Application.Contracts.Bookings.Models;

namespace NotificationEnrichmentService.Application.Contracts.Bookings;

public interface IBookingEnrichmentProcessor
{
    public Task ProcessCreatedBooking(ProcessCreatedBookingModel model, CancellationToken cancellationToken);

    public Task ProcessUpdatedBooking(ProcessUpdatedBookingModel model, CancellationToken cancellationToken);
}