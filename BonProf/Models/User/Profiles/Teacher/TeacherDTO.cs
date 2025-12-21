using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

/// <summary>
/// DTO pour l'affichage détaillé du profil d'un enseignant
/// </summary>
public class TeacherDetails
{
    /// <summary>
    /// Identifiant unique du profil enseignant
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Titre professionnel de l'enseignant
    /// </summary>
    /// <example>Professeur de Mathématiques</example>
    public string? Title { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionné avec 10 ans d'expérience</example>
    public string? Description { get; set; }

    /// <summary>
    /// Date de création du profil
    /// </summary>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Date de dernière mise à jour du profil
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
    public List<CursusDetails> Cursuses { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }

    public TeacherDetails() { }
    [SetsRequiredMembers]
    public TeacherDetails(Teacher teacher)
    {
        Title = teacher.Title;
        Description = teacher.Description;
        Id = teacher.Id;
        CreatedAt = teacher.CreatedAt;
        UpdatedAt = teacher.UpdatedAt;

        LinkedIn = teacher.LinkedIn;
        FaceBook = teacher.FaceBook;
        GitHub = teacher.GitHub;
        Twitter = teacher.Twitter;
        PriceIndicative = teacher.PriceIndicative;
        Cursuses = teacher.Cursuses.Select(c => new CursusDetails(c)).ToList();
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

    public UserUpdate? User { get; set; }

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
        profile.Title = Title;
        profile.Description = Description;
        profile.UpdatedAt = DateTimeOffset.UtcNow;
        profile.LinkedIn = LinkedIn;
        profile.FaceBook = FaceBook;
        profile.GitHub = GitHub;
        profile.Twitter = Twitter;
        profile.PriceIndicative = PriceIndicative;

        if (User is not null && profile.User is not null)
        {
            User.UpdateUser(profile.User);
        }
    }
}
