using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class TeacherDetailsDTO : BaseModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImgUrl { get; set; }

    public GenderDTO? Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    public ICollection<RoleAppDetailsDTO> Roles { get; set; }

    public TeacherDetailsDTO(ProfileTeacher teacher, List<RoleAppDetailsDTO>? roles)
    {
        Id = teacher.Id;
        FirstName = teacher.User.FirstName;
        LastName = teacher.User.LastName;
        Email = teacher.User.Email;
        Roles = roles;
        Gender = teacher.User.Gender is null ? null : new GenderDTO(teacher.User.Gender);
        Title = teacher.User.Title;
        Description = teacher.User.Description;
        PhoneNumber = teacher.User.PhoneNumber;
        DateOfBirth = teacher.User.DateOfBirth;
        ImgUrl = teacher.User.ImgUrl;
        CreatedAt = teacher.CreatedAt;
    }
}

