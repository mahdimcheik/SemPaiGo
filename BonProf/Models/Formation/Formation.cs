using System.Diagnostics.CodeAnalysis;
using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Formation : BaseModel
{
    public required string Title { get; set; }
    public required string Institute { get; set; }
    public required string Description { get; set; }
    public required DateTimeOffset DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }

    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public Formation() { }

    [SetsRequiredMembers]
    public Formation(FormationCreate newFormation)
    {
        Title = newFormation.Title;
        Institute = newFormation.Institute;
        Description = newFormation.Description;
        DateFrom = newFormation.DateFrom;
        DateTo = newFormation.DateTo;
        TeacherId = newFormation.TeacherId;
    }

    public void UpdateFormation(FormationUpdate formationUpdate)
    {
        Title = formationUpdate.Title;
        Institute = formationUpdate.Institute;
        Description = formationUpdate.Description;
        DateFrom = formationUpdate.DateFrom;
        DateTo = formationUpdate.DateTo;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
