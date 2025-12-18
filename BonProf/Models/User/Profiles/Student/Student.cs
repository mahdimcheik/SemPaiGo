using BonProf.Models;
using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Student: BaseModel
{
    public Guid ProfilId { get; set; }
    public Profile? Profile { get; set; }

    public ICollection<Reservation>? Reservations { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
