using SempaiGo.Models;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class StatusAccountDetailsDTO(StatusAccount gender)
{
    [Required]
    public Guid Id => gender.Id;
    [Required]
    public string Name => gender.Name;
    [Required]
    public string Color => gender.Color;
    public string? Icon => gender.Icon;
}
