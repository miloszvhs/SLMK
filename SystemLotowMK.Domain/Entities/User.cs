using Microsoft.AspNetCore.Identity;

namespace SystemLotowMK.Domain.Entities;

public class User : IdentityUser
{
    public virtual ICollection<Reservation> Reservations { get; set; }
}