using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class GenderDetailsDTO(Gender gender)
{
    public Guid Id => gender.Id;
    public string Name => gender.Name;
    public string Color => gender.Color;
    public string? Icon => gender.Icon;
}
