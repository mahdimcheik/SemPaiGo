using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models.Interfaces;

public interface IArchivable
{
    public DateTimeOffset? ArchivedAt { get; set; }
}

public interface IUpdateable
{
    public DateTimeOffset? UpdatedAt { get; set; }
}

public interface ICreatable
{
    public DateTimeOffset CreatedAt { get; set; }
}

public abstract class BaseModel : IUpdateable, ICreatable, IArchivable
{
    [Key]
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
}

public class BaseModelOption : BaseModel
{
    [Required]
    public  string Name { get; set; }

    [Required]
    public  string Color { get; set; }
    public string? Icon { get; set; }
}
