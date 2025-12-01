using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class Gender : BaseModelOption
{
}
public class GenderDTO(Gender gender)
{
    [Required]
    public Guid Id => gender.Id;
    [Required]
    public string Name => gender.Name;
    [Required]
    public string Color => gender.Color;
    public string? Icon => gender.Icon;
}
