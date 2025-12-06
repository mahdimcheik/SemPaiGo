using SempaiGo.Models;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class StatusAccountDetailsDTO(StatusAccount gender)
{
    public Guid Id => gender.Id;
    public string Name => gender.Name;
    public string Color => gender.Color;
    public string? Icon => gender.Icon;
}
