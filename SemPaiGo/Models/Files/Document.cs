using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Document :BaseModel
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string MimeType { get; set; }
    public long Size { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public Guid? TeacherId { get; set; }
    public ProfileTeacher? Teacher { get; set; }
    public Guid? StudentId { get; set; }
    public ProfileStudent? Student { get; set; }
    public Guid? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
}

