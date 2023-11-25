using GdscBookingBackend.Features.Reservations;
using Microsoft.EntityFrameworkCore;

namespace GdscBookingBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<ReservationModel> Reservations { get; set; } = null!;
}