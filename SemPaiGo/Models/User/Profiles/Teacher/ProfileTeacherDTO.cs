using System.ComponentModel.DataAnnotations;

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
    /// Identifiant de l'utilisateur associé
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Informations détaillées de l'utilisateur
    /// </summary>
    [Required]
    public UserDetails User { get; set; }

    /// <summary>
    /// Liste des formations de l'enseignant
    /// </summary>
    public List<FormationDetails> Formations { get; set; } = new();

    /// <summary>
    /// Date de création du profil
    /// </summary>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Date de dernière mise à jour du profil
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
    public List<LanguageDetails> Languages { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }

    public TeacherDetails() { }
    public TeacherDetails(ProfileTeacher profile)
    {
        Id = profile.Id;
        Title = profile.Title;
        Description = profile.Description;
        UserId = profile.UserId;
        User = new UserDetails(profile.User, null);
        Formations = profile.Formations.Select(f => new FormationDetails(f)).ToList();
        CreatedAt = profile.CreatedAt;
        UpdatedAt = profile.UpdatedAt;
        Languages = profile.Languages?.Select(l => new LanguageDetails(l)).ToList() ?? [];
        LinkedIn = profile.LinkedIn;
        FaceBook = profile.FaceBook;
        GitHub = profile.GitHub;
        Twitter = profile.Twitter;
    }

}

/// <summary>
/// DTO pour la création d'un profil enseignant
/// </summary>
public class TeacherProfileCreate
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
}

/// <summary>
/// DTO pour la mise à jour d'un profil enseignant
/// </summary>
public class TeacherProfileUpdate
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

    public void UpdateProfile(ProfileTeacher profile)
    {
        profile.Title = Title;
        profile.Description = Description;
        profile.UpdatedAt = DateTimeOffset.UtcNow;
        profile.LinkedIn = LinkedIn;
        profile.FaceBook = FaceBook;
        profile.GitHub = GitHub;
        profile.Twitter = Twitter;

        if (User is not null && profile.User is not null)
        {
            User.UpdateUser(profile.User);
        }
    }
}
