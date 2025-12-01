using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

/// <summary>
/// DTO pour l'affichage détaillé d'un rôle
/// </summary>
public class RoleAppDetailsDTO
{
    /// <summary>
    /// Identifiant unique du rôle
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Nom du rôle
    /// </summary>
    /// <example>Admin</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Nom normalisé du rôle (en majuscules)
    /// </summary>
    /// <example>ADMIN</example>
    public string? NormalizedName { get; set; }
    public string? Color { get; set; }
    public string DisplayName { get; set; }

    /// <summary>
    /// Date de création de l'enregistrement
    /// </summary>
    /// <example>2023-01-15T10:30:00Z</example>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Date de dernière mise à jour
    /// </summary>
    /// <example>2023-01-20T14:45:00Z</example>
    public DateTimeOffset? UpdatedAt { get; set; }

    public RoleAppDetailsDTO() { }

    public RoleAppDetailsDTO(RoleApp role)
    {
        Id = role.Id;
        Name = role.Name ?? string.Empty;
        NormalizedName = role.NormalizedName;
        DisplayName = role.DisplayName;
        CreatedAt = role.CreatedAt;
        UpdatedAt = role.UpdatedAt;
        Color = role.Color;
    }
}

/// <summary>
/// DTO pour la création d'un nouveau rôle
/// </summary>
public class RoleAppCreateDTO
{
    /// <summary>
    /// Nom du rôle
    /// </summary>
    /// <example>Manager</example>
    [Required(ErrorMessage = "Le nom du rôle est requis")]
    [StringLength(64, ErrorMessage = "Le nom ne peut pas dépasser 64 caractères")]
    public string Name { get; set; }
    public string? color { get; set; }
}

/// <summary>
/// DTO pour la mise à jour d'un rôle existant
/// </summary>
public class RoleAppUpdateDTO
{
    /// <summary>
    /// Nom du rôle
    /// </summary>
    /// <example>Manager</example>
    [Required(ErrorMessage = "Le nom du rôle est requis")]
    [StringLength(64, ErrorMessage = "Le nom ne peut pas dépasser 64 caractères")]
    public string Name { get; set; }
    public string? color { get; set; }


    public void UpdateRole(RoleApp role)
    {
        role.Name = Name;
        role.NormalizedName = Name.ToUpper();
        role.UpdatedAt = DateTimeOffset.UtcNow;
        role.Color = color;
    }
}
