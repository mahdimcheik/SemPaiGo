using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Language : BaseModelOption
{
    public ICollection<ProfileStudent>? Students{ get; set; }
    public ICollection<ProfileTeacher>? Teachers { get; set; }
}
