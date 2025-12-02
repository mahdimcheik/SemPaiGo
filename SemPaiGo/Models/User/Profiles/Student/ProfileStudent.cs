using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class ProfileStudent: BaseModel
{
    public Guid UserId { get; set; }
    public UserApp? User { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Formation> Formations { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Order> Orders { get; set; }
}
