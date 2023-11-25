namespace GdscBookingBackend.Features.Reservations;

public class ReservationModel
{
    public string Id { get; set; } = String.Empty;
    public DateTime Created { get; set; }
    public DateOnly Date { get; set; }
    public int Interval { get; set; }
    public string UserId { get; set; } = String.Empty;
    
}