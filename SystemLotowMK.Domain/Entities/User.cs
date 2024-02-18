using Microsoft.AspNetCore.Identity;

namespace SystemLotowMK.Domain.Entities;

public class User : IdentityUser
{
    public virtual List<Reservation> Reservations { get; set; }
}