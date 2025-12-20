using SemPaiGo.Models;
using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BonProf.Models;

public class Profile : BaseModel
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? ImgUrl { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    //gender
    [Required]
    [ForeignKey(nameof(Gender))]
    public Guid GenderId { get; set; }
    public Gender? Gender { get; set; }

    [SetsRequiredMembers]
    public Profile(ProfileCreate newProfile)
    {
        FirstName = newProfile.FirstName;
        LastName = newProfile.LastName;
        GenderId = newProfile.GenderId;
        ImgUrl = null;
    }
}
