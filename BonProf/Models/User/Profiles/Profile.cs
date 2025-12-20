using SemPaiGo.Models;
using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BonProf.Models;

public class Profile : BaseModel
{
    [Required]
    [MaxLength(64)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    [MaxLength(500)]
    public string? ImgUrl { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    public ICollection<Language> Languages { get; set; } = new List<Language>();

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public UserApp User { get; set; }

    //gender
    [Required]
    [ForeignKey(nameof(Gender))]
    public Guid GenderId { get; set; }
    public Gender? Gender { get; set; }

    // Parameterless constructor for EF Core
    public Profile()
    {
    }

    [SetsRequiredMembers]
    public Profile(ProfileCreate newProfile)
    {
        FirstName = newProfile.FirstName;
        LastName = newProfile.LastName;
        GenderId = newProfile.GenderId;
        ImgUrl = null;
    }
}
