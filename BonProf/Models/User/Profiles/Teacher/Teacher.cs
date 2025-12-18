using BonProf.Models;
using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Teacher : BaseModel
{
    public decimal PriceIndicative { get; set; }
    public Profile? Profile { get; set; }
    public ICollection<Cursus> Cursuses { get; set; } = new List<Cursus>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    public ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
