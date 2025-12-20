using SemPaiGo.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace SemPaiGo.Models;

public class Order : BaseModel
{
    [Required]
    public string OrderNumber { get; set; }
    [Required]
    [Column(TypeName = "timestamp with time zone")]
    public DateTimeOffset OrderDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    public decimal ReductionPercentage { get; set; } = 0m;
    public decimal ReductionAmount { get; set; } = 0m;
    [Required]
    [ForeignKey(nameof(Student))]
    public Guid StudentId { get; set; }
    public ProfileStudent? Student { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public Guid? PaymentId { get; set; }
    public Payment? Payment { get; set; }
}
