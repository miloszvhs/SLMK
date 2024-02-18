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

         SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Navigation(x => x.Reservations)
            .AutoInclude();
        
        modelBuilder.Entity<Flight>()
            .Navigation(x => x.Seats)
            .AutoInclude();
        
        modelBuilder.Entity<Seat>()
            .Navigation(x => x.Reservation)
            .AutoInclude();
        
        modelBuilder.Entity<Reservation>()
            .Navigation(x => x.Payment)
            .AutoInclude();
        
        modelBuilder.Entity<Payment>()
            .Navigation(x => x.Reservation)
            .AutoInclude();
        
        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 1,
                Departure = "Warsaw",
                Destination = "New York",
                DepartureTime = DateTime.UtcNow.AddDays(1),
                ArrivalTime = DateTime.UtcNow.AddDays(1).AddHours(8),
                Price = 1000,
                FlightNumber = "LO123"
            },
            new Flight
            {
                Id = 2,
                Departure = "New York",
                Destination = "Warsaw",
                DepartureTime = DateTime.UtcNow.AddDays(2),
                ArrivalTime = DateTime.UtcNow.AddDays(2).AddHours(8),
                Price = 900,
                FlightNumber = "LO123"
            }
        );
        
        modelBuilder.Entity<Seat>().HasData(
            new Seat
            {
                Id = 1,
                FlightId = 1,
                Number = "1A"
            },
            new Seat
            {
                Id = 2,
                FlightId = 1,
                Number = "1B"
            },
            new Seat
            {
                Id = 3,
                FlightId = 1,
                Number = "2A"
            },
            new Seat
            {
                Id = 4,
                FlightId = 1,
                Number = "2B"
            },
            new Seat
            {
                Id = 5,
                FlightId = 2,
                Number = "1A"
            },
            new Seat
            {
                Id = 6,
                FlightId = 2,
                Number = "1B"
            },
            new Seat
            {
                Id = 7,
                FlightId = 2,
                Number = "2A"
            },
            new Seat
            {
                Id = 8,
                FlightId = 2,
                Number = "2B"
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=slmk_main;User Id=SLMK;Password=Test12345;");
    }
}