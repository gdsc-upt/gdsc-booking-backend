namespace GdscBookingBackend.Features.Reservations;

public class ReservationRequest
{
    public DateOnly Date { get; set; }
    public int Interval { get; set; }
    public string UserId { get; set; } = String.Empty;
}