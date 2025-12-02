using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

public class Reservation : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid SlotId { get; set; }
    public Slot Slot { get; set; }

    public StatusReservation Status { get; set; }
    public Guid StatusId { get; set; } = HardCode.RESERVATION_PENDING;

    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid StudentId { get; set; }
    public ProfileStudent Student { get; set; }
    public Reservation()
    {
    }

    [SetsRequiredMembers]
    public Reservation(ReservationCreateDTO bookingCreate)
    {
        Title = bookingCreate.Title;
        Description = bookingCreate.Description ?? "";
        SlotId = bookingCreate.SlotId;
        StudentId = bookingCreate.StudentId;
    }
}