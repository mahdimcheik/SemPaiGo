using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Cursus : BaseModelOption
{
    public string? Description { get; set; }
    public Guid LevelId { get; set; }
    public LevelCursus? Level { get; set; }
    public Guid TeacherId { get; set; }
    public ProfileTeacher? Teacher { get; set; }
    public ICollection<CategoryCursus> Categories { get; set; } = new List<CategoryCursus>();

    public Cursus() { }

    public Cursus(CursusCreate newCursus, Guid teacherId,List<CategoryCursus> categories)
    {
        Name = newCursus.Name;
        Color = newCursus.Color;
        Icon = newCursus.Icon;
        Description = newCursus.Description;
        LevelId = newCursus.LevelId;
        TeacherId = teacherId;
        CreatedAt = DateTimeOffset.UtcNow;
        Categories = categories ?? [];
        UpdatedAt = null;
    }
}
