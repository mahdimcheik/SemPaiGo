using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Language : BaseModelOption
{
    public ICollection<ProfileTeacher>? Teachers { get; set; }
}
