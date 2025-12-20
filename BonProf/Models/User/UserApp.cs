using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BonProf.Models;
using Microsoft.AspNetCore.Identity;
using SempaiGo.Models;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;

namespace SemPaiGo.Models;

public class UserApp : IdentityUser<Guid>, IArchivable, IUpdateable, ICreatable
{
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    [Required]
    public required bool DataProcessingConsent { get; set; } = false;

    [Required]
    public required bool PrivacyPolicyConsent { get; set; } = false;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    // Status account
    [Required]
    [ForeignKey(nameof(Status))]
    public Guid StatusId { get; set; }
    public StatusAccount? Status { get; set; }

    // navigation to teacher / student
    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }
    public Profile Profile { get; set; }

    // roles
    public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; } =
        new List<IdentityUserRole<Guid>>();

    [SetsRequiredMembers]
    public UserApp() { }

    [SetsRequiredMembers]
    public UserApp(UserCreate newUser)
    {
        UserName = newUser.Email;
        Email = newUser.Email;
        DateOfBirth = newUser.DateOfBirth;
        StatusId = HardCode.ACCOUNT_PENDING;

        DataProcessingConsent = true;
        PrivacyPolicyConsent = true;

        Profile = new Profile(newUser.Profile);

        if (newUser.RoleId == HardCode.ROLE_TEACHER)
        {
            Teacher = new Teacher();
        }
        else
        {
            Student = new Student();
        }
    }
}
