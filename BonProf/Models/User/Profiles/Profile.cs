using SemPaiGo.Models;
using SemPaiGo.Models.Interfaces;

namespace BonProf.Models;

public class Profile :BaseModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }     
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public UserApp? User { get; set; }
    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }

    public ICollection<Language> Languages { get; set; } = new List<Language>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
