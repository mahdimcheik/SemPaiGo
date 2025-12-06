using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class ProfileTeacher : BaseModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public UserApp? User { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Cursus> Cursuses { get; set; } = new List<Cursus>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    public ICollection<Slot> Slots { get; set; } = new List<Slot>();

}
