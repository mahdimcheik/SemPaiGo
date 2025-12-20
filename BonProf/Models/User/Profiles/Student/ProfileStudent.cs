using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

[Table("ProfileStudents")]
public class ProfileStudent: BaseModel
{
    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    
    public UserApp? User { get; set; }
    
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    
    public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
