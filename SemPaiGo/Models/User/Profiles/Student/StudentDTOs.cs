using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class StudentDetailsDTO : BaseModel
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

    public StudentDetailsDTO(ProfileStudent student, List<RoleAppDetailsDTO>? roles)
    {
        Id = student.Id;
        FirstName = student.User.FirstName;
        LastName = student.User.LastName;
        Email = student.User.Email;
        Roles = roles;
        Gender = student.User.Gender is null ? null : new GenderDTO(student.User.Gender);
        Title = student.User.Title;
        Description = student.User.Description;
        PhoneNumber = student.User.PhoneNumber;
        DateOfBirth = student.User.DateOfBirth;
        ImgUrl = student.User.ImgUrl;
        CreatedAt = student.CreatedAt;
    }
}
