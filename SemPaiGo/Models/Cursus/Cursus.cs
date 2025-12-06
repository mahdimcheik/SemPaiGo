using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models;

public class Cursus : BaseModel
{
    public string? Description { get; set; }
    public string? ImgUrl { get; set; }
    public Guid LevelId { get; set; }
    public LevelCursus? Level { get; set; }
    public Guid TeacherId { get; set; }
    public ProfileTeacher? Teacher { get; set; }
    public ICollection<CategoryCursus> Categories { get; set; } = new List<CategoryCursus>();
}
