using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Document :BaseModel
{
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public required string MimeType { get; set; }
    public long Size { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public Guid? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
}

