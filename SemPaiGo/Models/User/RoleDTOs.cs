using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SemPaiGo.Models;

/// <summary>
/// DTO pour l'affichage détaillé d'un rôle
/// </summary>
public class RoleDetailsDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    [SetsRequiredMembers]
    public RoleDetailsDTO() { }


    [SetsRequiredMembers]   
    public RoleDetailsDTO(RoleApp role)
    {
        Id = role.Id;
        Name = role.Name ?? string.Empty;
        CreatedAt = role.CreatedAt;
        UpdatedAt = role.UpdatedAt;
        Color = role.Color;
    }
}
public class RoleAppCreateDTO
{
    [Required(ErrorMessage = "Le nom du rôle est requis")]
    [StringLength(64, ErrorMessage = "Le nom ne peut pas dépasser 64 caractères")]
    public required string Name { get; set; }
    public string? color { get; set; }
}

public class RoleAppUpdateDTO
{  
    [Required(ErrorMessage = "Le nom du rôle est requis")]
    [StringLength(64, ErrorMessage = "Le nom ne peut pas dépasser 64 caractères")]
    public required string Name { get; set; }
    public string? Color { get; set; }

    public void UpdateRole(RoleApp role)
    {
        role.Name = Name;
        role.NormalizedName = Name.ToUpper();
        role.UpdatedAt = DateTimeOffset.UtcNow;
        role.Color = Color;
    }
}
