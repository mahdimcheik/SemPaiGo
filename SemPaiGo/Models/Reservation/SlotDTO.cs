using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class SlotResponseDTO
{
    /// <summary>
    /// Identifiant unique du créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Date et heure de début du créneau
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required]
    public DateTimeOffset DateFrom { get; set; }

    /// <summary>
    /// Date et heure de fin du créneau
    /// </summary>
    /// <example>2023-01-15T11:30:00Z</example>
    [Required]
    public DateTimeOffset DateTo { get; set; }

    /// <summary>
    /// Identifiant de l'enseignant
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440001</example>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Informations de l'enseignant
    /// </summary>
    public UserDetailsDTO? Teacher { get; set; }

    /// <summary>
    /// Identifiant du type de créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440002</example>
    [Required]
    public Guid TypeId { get; set; }

    /// <summary>
    /// Informations du type de créneau
    /// </summary>
    public TypeSlotDetailsDTO? Type { get; set; }

    /// <summary>
    /// Date de création de l'enregistrement
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    /// <example>2023-01-20T14:45:00Z</example>
    public DateTimeOffset? UpdatedAt { get; set; }

    public ReservationDetailsDTO Reservation { get; set; }

    /// <summary>
    /// Indique si le créneau est disponible (non réservé)
    /// </summary>

    public SlotResponseDTO() { }

    public SlotResponseDTO(Slot slot)
    {
        Id = slot.Id;
        DateFrom = slot.DateFrom;
        DateTo = slot.DateTo;
        TeacherId = slot.TeacherId;
        TypeId = slot.TypeId;
        CreatedAt = slot.CreatedAt;
        UpdatedAt = slot.UpdatedAt;
        Reservation = slot.Reservation is not null ? new ReservationDetailsDTO(slot.Reservation) : null;

        if (slot.Teacher != null)
        {
            Teacher = new UserDetailsDTO(slot.Teacher, null);
        }

        if (slot.Type != null)
        {
            Type = new TypeSlotDetailsDTO(slot.Type);
        }
    }
}

public class SlotDetailsDTO
{
    /// <summary>
    /// Identifiant unique du créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Date et heure de début du créneau
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required]
    public DateTimeOffset DateFrom { get; set; }

    /// <summary>
    /// Date et heure de fin du créneau
    /// </summary>
    /// <example>2023-01-15T11:30:00Z</example>
    [Required]
    public DateTimeOffset DateTo { get; set; }

    /// <summary>
    /// Informations de l'enseignant
    /// </summary>
    public UserDetailsDTO? Teacher { get; set; }

    /// <summary>
    /// Identifiant du type de créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440002</example>
    [Required]
    public Guid TypeId { get; set; }

    /// <summary>
    /// Informations du type de créneau
    /// </summary>
    public TypeSlotDetailsDTO? Type { get; set; }

    /// <summary>
    /// Indique si le créneau est disponible (non réservé)
    /// </summary>

    public SlotDetailsDTO() { }

    public SlotDetailsDTO(Slot slot)
    {
        Id = slot.Id;
        DateFrom = slot.DateFrom;
        DateTo = slot.DateTo;
        TypeId = slot.TypeId;

        if (slot.Teacher != null)
        {
            Teacher = new UserDetailsDTO(slot.Teacher, null);
        }

        if (slot.Type != null)
        {
            Type = new TypeSlotDetailsDTO(slot.Type);
        }
    }
}

/// <summary>
/// DTO pour la création d'un nouveau créneau
/// </summary>
public class SlotCreateDTO
{
    /// <summary>
    /// Date et heure de début du créneau
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required(ErrorMessage = "La date de début est requise")]
    public DateTimeOffset DateFrom { get; set; }

    /// <summary>
    /// Date et heure de fin du créneau
    /// </summary>
    /// <example>2023-01-15T11:30:00Z</example>
    [Required(ErrorMessage = "La date de fin est requise")]
    public DateTimeOffset DateTo { get; set; }

    /// <summary>
    /// Identifiant de l'enseignant
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440001</example>
    [Required(ErrorMessage = "L'identifiant de l'enseignant est requis")]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Identifiant du type de créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440002</example>
    [Required(ErrorMessage = "L'identifiant du type de créneau est requis")]
    public Guid TypeId { get; set; }
}

/// <summary>
/// DTO pour la mise à jour d'un créneau existant
/// </summary>
public class SlotUpdateDTO
{
    /// <summary>
    /// Date et heure de début du créneau
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required(ErrorMessage = "La date de début est requise")]
    public DateTimeOffset DateFrom { get; set; }

    /// <summary>
    /// Date et heure de fin du créneau
    /// </summary>
    /// <example>2023-01-15T11:30:00Z</example>
    [Required(ErrorMessage = "La date de fin est requise")]
    public DateTimeOffset DateTo { get; set; }

    /// <summary>
    /// Identifiant de l'enseignant
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440001</example>
    [Required(ErrorMessage = "L'identifiant de l'enseignant est requis")]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Identifiant du type de créneau
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440002</example>
    [Required(ErrorMessage = "L'identifiant du type de créneau est requis")]
    public Guid TypeId { get; set; }

    public void UpdateSlot(Slot slot)
    {
        slot.DateFrom = DateFrom;
        slot.DateTo = DateTo;
        slot.TeacherId = TeacherId;
        slot.TypeId = TypeId;
        slot.UpdatedAt = DateTimeOffset.UtcNow;
    }
}
