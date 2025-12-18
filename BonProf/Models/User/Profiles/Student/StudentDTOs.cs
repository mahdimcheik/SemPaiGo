using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class StudentDetails
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
    public List<CursusDetails> Cursuses { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public decimal PriceIndicative { get; set; }

    public StudentDetails() { }   
    public StudentDetails(Student student) { }   

}