using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class ProfileTeacher : BaseModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public UserApp? User { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Cursus> Cursuses { get; set; }
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Formation> Formations { get; set; }
    public ICollection<Slot> Slots { get; set; }

}
