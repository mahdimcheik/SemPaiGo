using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class TeacherPayout : BaseModel
{
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public DateTimeOffset PaidAt { get; set; }
    public Guid StatusId { get; set; }
    public StatusTransaction? Status { get; set; }
}
