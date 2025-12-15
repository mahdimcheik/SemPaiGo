using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

public class StudentDetails : BaseModel
{
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string Email { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImgUrl { get; set; }

    public GenderDetails? Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    public ICollection<RoleDetails> Roles { get; set; }

    [SetsRequiredMembers]
    public StudentDetails(ProfileStudent student, List<RoleDetails>? roles)
    {
        Id = student.Id;
        FirstName = student.User.FirstName;
        LastName = student.User.LastName;
        Email = student.User.Email;
        Roles = roles;
        Gender = student.User.Gender is null ? null : new GenderDetails(student.User.Gender);
        PhoneNumber = student.User.PhoneNumber;
        DateOfBirth = student.User.DateOfBirth;
        ImgUrl = student.User.ImgUrl;
        CreatedAt = student.CreatedAt;
    }
}
