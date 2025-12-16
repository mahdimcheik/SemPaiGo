using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des types de créneaux
/// </summary>
public class TypeSlotsService(MainContext context)
{
    /// <summary>
    /// Récupère tous les types de créneaux
    /// </summary>
    /// <returns>Liste des types de créneaux</returns>
    public async Task<Response<List<TypeSlotDetails>>> GetAllTypeSlotsAsync()
    {
        try
        {
            var typeSlots = await context
                .TypeSlots.AsNoTracking()
                .Where(t => t.ArchivedAt == null)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TypeSlotDetails(t))
                .ToListAsync();

            return new Response<List<TypeSlotDetails>>
            {
                Status = 200,
                Message = "Types de créneaux récupérés avec succès",
                Data = typeSlots,
                Count = typeSlots.Count,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<TypeSlotDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des types de créneaux: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Récupère un type de créneau par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du type de créneau</param>
    /// <returns>Type de créneau trouvé</returns>
    public async Task<Response<TypeSlotDetails>> GetTypeSlotByIdAsync(Guid id)
    {
        try
        {
            var typeSlot = await context
                .TypeSlots.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.ArchivedAt == null);

            if (typeSlot == null)
            {
                return new Response<TypeSlotDetails>
                {
                    Status = 404,
                    Message = "Type de créneau non trouvé",
                    Data = null,
                };
            }

            return new Response<TypeSlotDetails>
            {
                Status = 200,
                Message = "Type de créneau récupéré avec succès",
                Data = new TypeSlotDetails(typeSlot),
            };
        }
        catch (Exception ex)
        {
            return new Response<TypeSlotDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du type de créneau: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Crée un nouveau type de créneau
    /// </summary>
    /// <param name="typeSlotDto">Données du type de créneau à créer</param>
    /// <returns>Type de créneau créé</returns>
    public async Task<Response<TypeSlotDetails>> CreateTypeSlotAsync(TypeSlotCreate typeSlotDto)
    {
        try
        {
            // Vérifier si un type de créneau avec le même nom existe déjà
            var existingTypeSlot = await context.TypeSlots.AnyAsync(t =>
                t.Name.ToLower() == typeSlotDto.Name.ToLower() && t.ArchivedAt == null
            );

            if (existingTypeSlot)
            {
                return new Response<TypeSlotDetails>
                {
                    Status = 400,
                    Message = "Un type de créneau avec ce nom existe déjà",
                    Data = null,
                };
            }

            var typeSlot = new TypeSlot
            {
                Id = Guid.NewGuid(),
                Name = typeSlotDto.Name,
                Color = typeSlotDto.Color,
                Icon = typeSlotDto.Icon,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            context.TypeSlots.Add(typeSlot);
            await context.SaveChangesAsync();

            return new Response<TypeSlotDetails>
            {
                Status = 201,
                Message = "Type de créneau créé avec succès",
                Data = new TypeSlotDetails(typeSlot),
            };
        }
        catch (Exception ex)
        {
            return new Response<TypeSlotDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création du type de créneau: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Met à jour un type de créneau existant
    /// </summary>
    /// <param name="id">Identifiant du type de créneau</param>
    /// <param name="typeSlotDto">Nouvelles données du type de créneau</param>
    /// <returns>Type de créneau mis à jour</returns>
    public async Task<Response<TypeSlotDetails>> UpdateTypeSlotAsync(
        TypeSlotUpdate typeSlotDto
    )
    {
        try
        {
            var typeSlot = await context.TypeSlots.FirstOrDefaultAsync(t =>
                t.Id == typeSlotDto.Id && t.ArchivedAt == null
            );

            if (typeSlot == null)
            {
                return new Response<TypeSlotDetails>
                {
                    Status = 404,
                    Message = "Type de créneau non trouvé",
                    Data = null,
                };
            }

            // Vérifier si le nom n'existe pas déjà pour un autre type de créneau
            var existingTypeSlot = await context.TypeSlots.AnyAsync(t =>
                t.Name.ToLower() == typeSlotDto.Name.ToLower()
                && t.Id != typeSlotDto.Id
                && t.ArchivedAt == null
            );

            if (existingTypeSlot)
            {
                return new Response<TypeSlotDetails>
                {
                    Status = 400,
                    Message = "Un autre type de créneau avec ce nom existe déjà",
                    Data = null,
                };
            }

            typeSlotDto.UpdateTypeSlot(typeSlot);

            await context.SaveChangesAsync();

            return new Response<TypeSlotDetails>
            {
                Status = 200,
                Message = "Type de créneau mis à jour avec succès",
                Data = new TypeSlotDetails(typeSlot),
            };
        }
        catch (Exception ex)
        {
            return new Response<TypeSlotDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour du type de créneau: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Archive un type de créneau (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du type de créneau</param>
    /// <returns>Résultat de l'opération</returns>
    public async Task<Response<bool>> DeleteTypeSlotAsync(Guid id)
    {
        try
        {
            var typeSlot = await context.TypeSlots.FirstOrDefaultAsync(t =>
                t.Id == id && t.ArchivedAt == null
            );

            if (typeSlot == null)
            {
                return new Response<bool>
                {
                    Status = 404,
                    Message = "Type de créneau non trouvé",
                    Data = false,
                };
            }

            typeSlot.ArchivedAt = DateTimeOffset.UtcNow;
            typeSlot.UpdatedAt = DateTimeOffset.UtcNow;

            await context.SaveChangesAsync();

            return new Response<bool>
            {
                Status = 200,
                Message = "Type de créneau supprimé avec succès",
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new Response<bool>
            {
                Status = 500,
                Message = $"Erreur lors de la suppression du type de créneau: {ex.Message}",
                Data = false,
            };
        }
    }
}
