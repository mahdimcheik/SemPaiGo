using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Order : BaseModel
{
    public string OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal ReductionPercentage { get; set; }
    public decimal ReductionAmount { get; set; }
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public Guid? PaymentId { get; set; }
    public Payment? Payment { get; set; }
}
