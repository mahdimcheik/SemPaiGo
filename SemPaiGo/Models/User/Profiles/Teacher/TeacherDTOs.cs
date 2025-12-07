using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

public class TeacherDetails : ICreatable
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; } = null!;
    public DateTimeOffset? DateOfBirth { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImgUrl { get; set; }
    public GenderDetails? Gender { get; set; }
    public ICollection<RoleDetails> Roles { get; set; } = new List<RoleDetails>();
    public DateTimeOffset CreatedAt { get ; set ; }

    [SetsRequiredMembers]
    public TeacherDetails(ProfileTeacher teacher, List<RoleDetails>? roles)
    {
        Id = teacher.Id;
        FirstName = teacher.User?.FirstName ?? "";
        LastName = teacher.User?.LastName ?? "";
        Email = teacher.User?.Email ?? "";
        Roles = roles ?? [];
        Gender = teacher.User?.Gender is null ? null : new GenderDetails(teacher.User.Gender);
        Title = teacher.Title;
        Description = teacher.Description;
        PhoneNumber = teacher.User?.PhoneNumber;
        DateOfBirth = teacher.User?.DateOfBirth;
        ImgUrl = teacher.User?.ImgUrl;
        CreatedAt = teacher.CreatedAt;
    }
}

