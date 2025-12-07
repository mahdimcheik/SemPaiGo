using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;


public class ReservationDetailsDTO(Reservation booking)
{
    public Guid Id => booking.Id;
    public string Title => booking.Title;
    public string Description => booking.Description;
    public StatusReservationOutput? Status =>
        booking.Status is not null ? new StatusReservationOutput(booking.Status) : null;
    public StudentDetails? Student =>
        booking.Student is not null ? new StudentDetails(booking.Student, null) : null;
    public SlotDetailsDTO? Slot => booking.Slot is not null ? new SlotDetailsDTO(booking.Slot) : null;
}

public class ReservationCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public UserDetails? Student { get; set; }
    public Guid SlotId { get; set; }
    public Guid StudentId { get; set; }
}

public class BookingUpdateDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    public void UpdateModel(Reservation reservation)
    {
        reservation.Title = Title;
        reservation.Description = Description;
    }
}