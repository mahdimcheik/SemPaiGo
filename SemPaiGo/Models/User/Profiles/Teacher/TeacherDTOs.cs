using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

public class TeacherDetailsDTO : ICreatable
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
    public GenderDetailsDTO? Gender { get; set; }
    public ICollection<RoleDetailsDTO> Roles { get; set; } = new List<RoleDetailsDTO>();
    public DateTimeOffset CreatedAt { get ; set ; }

    [SetsRequiredMembers]
    public TeacherDetailsDTO(ProfileTeacher teacher, List<RoleDetailsDTO>? roles)
    {
        Id = teacher.Id;
        FirstName = teacher.User?.FirstName ?? "";
        LastName = teacher.User?.LastName ?? "";
        Email = teacher.User?.Email ?? "";
        Roles = roles ?? [];
        Gender = teacher.User?.Gender is null ? null : new GenderDetailsDTO(teacher.User.Gender);
        Title = teacher.Title;
        Description = teacher.Description;
        PhoneNumber = teacher.User?.PhoneNumber;
        DateOfBirth = teacher.User?.DateOfBirth;
        ImgUrl = teacher.User?.ImgUrl;
        CreatedAt = teacher.CreatedAt;
    }
}

