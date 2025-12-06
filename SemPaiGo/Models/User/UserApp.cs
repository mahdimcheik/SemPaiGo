using Microsoft.AspNetCore.Identity;
using SempaiGo.Models;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

public class UserApp : IdentityUser<Guid>, IArchivable, IUpdateable, ICreatable
{    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
    public string? ImgUrl { get; set; }
    public required bool DataProcessingConsent { get; set; } = false;
    public required bool PrivacyPolicyConsent { get; set; } = false;
    public DateTimeOffset? ArchivedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    // roles
    public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } =new List<IdentityUserRole<Guid>>();

    //gender
    public Guid GenderId { get; set; }
    public Gender? Gender { get; set; }

    // Status account
    public Guid StatusId { get; set; }
    public StatusAccount? Status { get; set; }
    [SetsRequiredMembers]

    public UserApp()
    {
        
    }

    [SetsRequiredMembers]
    public UserApp(UserCreateDTO newUser)
    {
        UserName = newUser.Email;
        Email = newUser.Email;
        FirstName = newUser.FirstName;
        LastName = newUser.LastName;
        DateOfBirth = newUser.DateOfBirth;
        GenderId = newUser.GenderId;
        PhoneNumber = newUser.PhoneNumber;
        StatusId = HardCode.ACCOUNT_PENDING;

        DataProcessingConsent = true;
        PrivacyPolicyConsent = true;
    }
}



