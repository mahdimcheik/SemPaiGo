using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class TeacherWalletTransaction : BaseModel
{
    public decimal Amount { get; set; }
    public Guid TypeId { get; set; }
    public TypeTeacherTransaction Type { get; set; }
    public Guid? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
}
