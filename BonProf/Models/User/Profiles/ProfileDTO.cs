using SemPaiGo.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BonProf.Models;

public class ProfileDetails
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }
    [MaxLength(500)]
    public string? ImgUrl { get; set; }  
    public required GenderDetails? Gender { get; set; }

    public List<FormationDetails> Formations { get; set; }
    public List<AddressDetails> Addresses { get; set; }
    public List<LanguageDetails> Languages { get; set; }

    [SetsRequiredMembers]
    public ProfileDetails(Profile profile)
    {
        FirstName = profile.FirstName;
        LastName = profile.LastName;
        DateOfBirth = profile.DateOfBirth;
        Gender = profile.Gender is not null ? new GenderDetails(profile.Gender) : null;
        ImgUrl = profile.ImgUrl;
        Formations = profile.Formations.Select(f => new FormationDetails(f)).ToList();
        Addresses = profile.Addresses.Select(a => new AddressDetails(a)).ToList();
        Languages = profile.Languages.Select(l => new LanguageDetails(l)).ToList();
    }
}
public class ProfileCreate
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    //gender
    [Required]
    public required Guid GenderId { get; set; }
}

public class ProfileUpdate
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    //gender
    [Required]
    public required Guid GenderId { get; set; }
}

