using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class CategoryCursus : BaseModelOption
{
    public ICollection<Cursus> Cursuses { get; set; } = new List<Cursus>();
}
