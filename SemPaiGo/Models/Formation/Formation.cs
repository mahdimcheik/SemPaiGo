using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Formation : BaseModel
{
    public string Title { get; set; }
    public string Institute { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }

    public Guid? TeacherId { get; set; }
    public ProfileTeacher Teacher { get; set; }
    public Guid? StudentId { get; set; }
    public ProfileStudent Student { get; set; }
}
