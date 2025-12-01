using Microsoft.AspNetCore.Identity;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class UserApp : IdentityUser<Guid>, IArchivable, IUpdateable, ICreatable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public bool DataProcessingConsent { get; set; } = false;
    public bool PrivacyPolicyConsent { get; set; } = false;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImgUrl { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    // roles
    public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

    //gender
    public Guid GenderId { get; set; }
    public Gender? Gender { get; set; }

    // Status account
    public Guid StatusId { get; set; }
}



