using BonProf.Models;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

/// <summary>
/// DTO pour l'affichage détaillé du profil d'un enseignant
/// </summary>
public class TeacherDetails
{
    [Required]
    public Guid Id { get; set; }
    public decimal PriceIndicative { get; set; }
    public List<CursusDetails> Cursuses { get; set; }
    public List<FormationDetails> Formations { get; set; } = new();
    public ICollection<SlotDetails> Slots { get; set; } = new List<SlotDetails>();

    public TeacherDetails() { }
    public TeacherDetails(Teacher teacher)
    {
        Id = teacher.Id;
        PriceIndicative = teacher.PriceIndicative;
        Formations = teacher.Formations.Select(f => new FormationDetails(f)).ToList();
        Cursuses = teacher.Cursuses?.Select(l => new CursusDetails(l)).ToList() ?? [];
        Cursuses = teacher.Cursuses?.Select(l => new CursusDetails(l)).ToList() ?? [];
    }
}

/// <summary>
/// DTO pour la création d'un profil enseignant
/// </summary>
public class TeacherCreate
{
    /// <summary>
    /// Titre professionnel de l'enseignant
    /// </summary>
    /// <example>Professeur de Mathématiques</example>
    [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères")]
    public string? Title { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionné avec 10 ans d'expérience</example>
    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
    public string? Description { get; set; }

    /// <summary>
    /// Identifiant de l'utilisateur associé
    /// </summary>
    [Required(ErrorMessage = "L'identifiant utilisateur est requis")]
    public Guid UserId { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }
}

/// <summary>
/// DTO pour la mise à jour d'un profil enseignant
/// </summary>
public class TeacherUpdate
{
    /// <summary>
    /// Identifiant du profil à mettre à jour
    /// </summary>
    [Required(ErrorMessage = "L'identifiant est requis")]
    public Guid Id { get; set; }

    /// <summary>
    /// Titre professionnel de l'enseignant
    /// </summary>
    /// <example>Professeur de Mathématiques</example>
    [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères")]
    public string? Title { get; set; }

    public UserUpdateInput? User { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionné avec 10 ans d'expérience</example>
    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
    public string? Description { get; set; }

    public List<Guid> LanguagesIds { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }


    public void UpdateProfile(Teacher profile)
    {
        //profile.Title = Title;
        //profile.Description = Description;
        //profile.UpdatedAt = DateTimeOffset.UtcNow;
        //profile.LinkedIn = LinkedIn;
        //profile.FaceBook = FaceBook;
        //profile.GitHub = GitHub;
        //profile.Twitter = Twitter;
        profile.PriceIndicative = PriceIndicative;

        //if (User is not null && profile.User is not null)
        //{
        //    User.UpdateUser(profile.User);
        //}
    }
}
