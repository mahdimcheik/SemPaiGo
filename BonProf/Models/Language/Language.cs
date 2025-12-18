using BonProf.Models;
using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Language : BaseModelOption
{
    public ICollection<Teacher>? Teachers { get; set; }
    public ICollection<Student>? Students { get; set; }
    public ICollection<Profile>? Profiles { get; set; }

}
