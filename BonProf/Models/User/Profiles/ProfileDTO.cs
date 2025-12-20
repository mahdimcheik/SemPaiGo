using SemPaiGo.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BonProf.Models;

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

public class ProfileDetails
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    //gender
    [Required]
    public required GenderDetails Gender { get; set; }

    [SetsRequiredMembers]
    public ProfileDetails(Profile profile)
    {
        FirstName = profile.FirstName;
        LastName = profile.LastName;
        DateOfBirth = profile.DateOfBirth;
        Gender = new GenderDetails(profile.Gender);
    }
}