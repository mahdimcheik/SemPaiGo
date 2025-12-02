using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class ProfileStudent: BaseModel
{
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Formation> Formations { get; set; }

}
