using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BonProf.Models;

namespace SemPaiGo.Models;

/// <summary>
/// DTO pour l'affichage d�taill� du profil d'un enseignant
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
    /// <example>Professeur de Math�matiques</example>
    public string? Title { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionn� avec 10 ans d'exp�rience</example>
    public string? Description { get; set; }

    /// <summary>
    /// Date de cr�ation du profil
    /// </summary>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Date de derni�re mise � jour du profil
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
/// DTO pour la cr�ation d'un profil enseignant
/// </summary>
public class TeacherCreate
{
    /// <summary>
    /// Titre professionnel de l'enseignant
    /// </summary>
    /// <example>Professeur de Math�matiques</example>
    [StringLength(200, ErrorMessage = "Le titre ne peut pas d�passer 200 caract�res")]
    public string? Title { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionn� avec 10 ans d'exp�rience</example>
    [StringLength(1000, ErrorMessage = "La description ne peut pas d�passer 1000 caract�res")]
    public string? Description { get; set; }

    /// <summary>
    /// Identifiant de l'utilisateur associ�
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
/// DTO pour la mise � jour d'un profil enseignant
/// </summary>
public class TeacherUpdate
{
    /// <summary>
    /// Titre professionnel de l'enseignant
    /// </summary>
    /// <example>Professeur de Math�matiques</example>
    [StringLength(200, ErrorMessage = "Le titre ne peut pas d�passer 200 caract�res")]
    public string? Title { get; set; }

    /// <summary>
    /// Description du profil de l'enseignant
    /// </summary>
    /// <example>Enseignant passionn� avec 10 ans d'exp�rience</example>
    [StringLength(1000, ErrorMessage = "La description ne peut pas d�passer 1000 caract�res")]
    public string? Description { get; set; }
    [Required]
    [MaxLength(64)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }
    
    [Required]
    public Guid GenderId { get; set; }

    /// <summary>
    /// Liste des identifiants de langues
    /// </summary>
    public List<Guid> LanguageIds { get; set; } = new();

    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }
}
