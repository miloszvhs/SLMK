using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Infrastructure.ApplicationContexts;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext()
    {
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Flight>().ToTable("Flights");
         modelBuilder.Entity<Payment>().ToTable("Payments");
         modelBuilder.Entity<Reservation>().ToTable("Reservations");
         modelBuilder.Entity<User>().ToTable("Users");
         modelBuilder.Entity<Seat>().ToTable("Seats");
        
         modelBuilder.Entity<Flight>()
             .HasMany(f => f.Seats)
             .WithOne(s => s.Flight)
             .HasForeignKey(s => s.FlightId);
        
         modelBuilder.Entity<Seat>()
             .HasOne(s => s.Reservation)
             .WithOne(r => r.Seat)
             .HasForeignKey<Reservation>(r => r.SeatId);
        
         modelBuilder.Entity<Reservation>()
             .HasOne(r => r.User)
             .WithMany(u => u.Reservations)
             .HasForeignKey(r => r.UserId);
        
         modelBuilder.Entity<Payment>()
             .HasOne(p => p.Reservation)
             .WithOne(r => r.Payment)
             .HasForeignKey<Payment>(p => p.ReservationId);
        
         modelBuilder.Entity<User>()
             .HasMany(u => u.Reservations)
             .WithOne(r => r.User)
             .HasForeignKey(r => r.UserId);
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=slmk_main;User Id=SLMK;Password=Test12345;");
    }
}