using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Payment : BaseModel
{
    public decimal Amount { get; set; }
    public Guid MethodId { get; set; }
    public PaymentMethod Method { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public Guid TransactionRef { get; set; }
}
