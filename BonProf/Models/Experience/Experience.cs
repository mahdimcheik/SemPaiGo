using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;


public class Experience: BaseModel
{
    [Required]
    required public string Title { get; set; }
    [Required]
    required public string Description { get; set; }

    [Required]
    required public string Company { get; set; }
    [Required]
    required public DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }

    public Guid TeacherId { get; set; }
    public ProfileTeacher? Teacher { get; set; }
}
