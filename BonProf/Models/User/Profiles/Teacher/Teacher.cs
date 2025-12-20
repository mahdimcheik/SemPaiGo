using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Teacher : BaseModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public UserApp? User { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }

    public ICollection<Cursus> Cursuses { get; set; } = new List<Cursus>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Slot> Slots { get; set; } = new List<Slot>();    
}
