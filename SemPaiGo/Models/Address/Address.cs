using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemPaiGo.Models;

public class Address : BaseModel
{
    [Required]
    public required string Street { get; set; }

    [Required]
    public required string City { get; set; }

    [Required]
    public required string State { get; set; }

    [Required]
    public required string Country { get; set; }

    [Required]
    public required string ZipCode { get; set; }
    public string? AdditionalInfo { get; set; }
    public float? Longitude { get; set; }
    public float? Latitude { get; set; }
    public Guid? TeacherId { get; set; }
    public ProfileTeacher? Teacher { get; set; }

    public Guid? StudentId { get; set; }
    public ProfileStudent? Student { get; set; }
}
