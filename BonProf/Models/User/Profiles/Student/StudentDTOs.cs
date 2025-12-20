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
}
