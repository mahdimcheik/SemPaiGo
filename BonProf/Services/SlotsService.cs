using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Utilities;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des créneaux
/// </summary>
public class SlotsService(MainContext context)
{
    /// <summary>
    /// Ajoute un nouveau créneau pour un enseignant
    /// </summary>
    /// <param name="slotDto">Données du créneau à créer</param>
    /// <param name="userPrincipal">Principal de l'utilisateur connecté</param>
    /// <returns>Créneau créé</returns>
    public async Task<Response<SlotDetails>> AddSlotByTeacherAsync(
        SlotCreate slotDto,
        ClaimsPrincipal userPrincipal
    )
    {
        try
        {
            // Récupérer l'utilisateur connecté
            var user = CheckUser.GetUserFromClaim(userPrincipal, context);
            if (user == null)
            {
                return new Response<SlotDetails>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = null,
                };
            }

            // Vérifier que l'utilisateur est bien un enseignant
            var teacher = await context.Teachers.FirstOrDefaultAsync(t =>
                t.UserId == user.Id
            );

            if (teacher == null)
            {
                return new Response<SlotDetails>
                {
                    Status = 403,
                    Message = "Vous devez être un enseignant pour créer des créneaux",
                    Data = null,
                };
            }

            // Validation des dates
            if (slotDto.DateFrom >= slotDto.DateTo)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "La date de fin doit être postérieure à la date de début",
                    Data = null,
                };
            }

            // Vérifier que le créneau est dans le futur
            if (slotDto.DateFrom < DateTimeOffset.UtcNow)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "Le créneau doit être dans le futur",
                    Data = null,
                };
            }

            // Vérifier que le type de créneau existe

            var typeExists = await context.TypeSlots.AnyAsync(t =>
                t.Id == slotDto.TypeId && t.ArchivedAt == null
            );
            if (!typeExists)
            {
                return new Response<SlotDetails>
                {
                    Status = 404,
                    Message = "Type de créneau non trouvé",
                    Data = null,
                };
            }

            // Vérifier qu'il n'y a pas de chevauchement avec un autre créneau du même enseignant
            var hasOverlap = await context.Slots.AnyAsync(s =>
                s.TeacherId == teacher.Id
                && s.ArchivedAt == null
                && (
                    (slotDto.DateFrom >= s.DateFrom && slotDto.DateFrom < s.DateTo)
                    || (slotDto.DateTo > s.DateFrom && slotDto.DateTo <= s.DateTo)
                    || (slotDto.DateFrom <= s.DateFrom && slotDto.DateTo >= s.DateTo)
                )
            );

            if (hasOverlap)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "Ce créneau chevauche un créneau existant",
                    Data = null,
                };
            }

            var slot = new Slot
            {
                Id = Guid.NewGuid(),
                DateFrom = slotDto.DateFrom,
                DateTo = slotDto.DateTo,
                TeacherId = teacher.Id,
                TypeId = slotDto.TypeId,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            context.Slots.Add(slot);
            await context.SaveChangesAsync();

            // Recharger avec les relations pour la réponse
            var createdSlot = await context
                .Slots.Include(s => s.Teacher)
                .ThenInclude(t => t.User)
                .Include(s => s.Type)
                .FirstAsync(s => s.Id == slot.Id);

            return new Response<SlotDetails>
            {
                Status = 201,
                Message = "Créneau créé avec succès",
                Data = new SlotDetails(createdSlot),
            };
        }
        catch (Exception ex)
        {
            return new Response<SlotDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création du créneau: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Ajoute un nouveau créneau pour un enseignant
    /// </summary>
    /// <param name="slotDto">Données du créneau à créer</param>
    /// <param name="userPrincipal">Principal de l'utilisateur connecté</param>
    /// <returns>Créneau créé</returns>
    public async Task<Response<SlotDetails>> UpdateSlotByTeacherAsync(
        SlotUpdate slotDto,
        ClaimsPrincipal userPrincipal
    )
    {
        try
        {
            // Récupérer l'utilisateur connecté
            var user = CheckUser.GetUserFromClaim(userPrincipal, context);
            if (user == null)
            {
                return new Response<SlotDetails>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = null,
                };
            }

            // Vérifier que l'utilisateur est bien un enseignant
            var teacher = await context.Teachers            .FirstOrDefaultAsync(t =>
                t.UserId == user.Id
            );

            if (teacher == null)
            {
                return new Response<SlotDetails>
                {
                    Status = 403,
                    Message = "Vous devez être un enseignant pour créer des créneaux",
                    Data = null,
                };
            }

            // Validation des dates
            if (slotDto.DateFrom >= slotDto.DateTo)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "La date de fin doit être postérieure à la date de début",
                    Data = null,
                };
            }

            // Vérifier que le créneau est dans le futur
            if (slotDto.DateFrom < DateTimeOffset.UtcNow)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "Le créneau doit être dans le futur",
                    Data = null,
                };
            }

            // Vérifier que le type de créneau existe

            var typeExists = await context.TypeSlots.AnyAsync(t =>
                t.Id == slotDto.TypeId && t.ArchivedAt == null
            );
            if (!typeExists)
            {
                return new Response<SlotDetails>
                {
                    Status = 404,
                    Message = "Type de créneau non trouvé",
                    Data = null,
                };
            }

            // Vérifier qu'il n'y a pas de chevauchement avec un autre créneau du même enseignant
            var hasOverlap = await context.Slots.AnyAsync(s =>
                s.TeacherId == teacher.Id
                && s.Id != slotDto.Id
                && s.ArchivedAt == null
                && (
                    (slotDto.DateFrom >= s.DateFrom && slotDto.DateFrom < s.DateTo)
                    || (slotDto.DateTo > s.DateFrom && slotDto.DateTo <= s.DateTo)
                    || (slotDto.DateFrom <= s.DateFrom && slotDto.DateTo >= s.DateTo)
                )
            );

            if (hasOverlap)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "Ce créneau chevauche un créneau existant",
                    Data = null,
                };
            }

            var slot = await context.Slots.FirstOrDefaultAsync(x => x.Id == slotDto.Id);

            if (slot is null)
            {
                return new Response<SlotDetails>
                {
                    Status = 400,
                    Message = "Ce créneau n'existe pas/plus",
                    Data = null,
                };
            }
            slotDto.UpdateSlot(slot);
            await context.SaveChangesAsync();

            return new Response<SlotDetails>
            {
                Status = 201,
                Message = "Créneau créé avec succès",
                Data = new SlotDetails(slot),
            };
        }
        catch (Exception ex)
        {
            return new Response<SlotDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création du créneau: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Supprime un créneau pour un enseignant (suppression logique)
    /// </summary>
    /// <param name="slotId">Identifiant du créneau</param>
    /// <param name="userPrincipal">Principal de l'utilisateur connecté</param>
    /// <returns>Résultat de l'opération</returns>
    public async Task<Response<bool>> RemoveSlotByTeacherAsync(
        Guid slotId,
        ClaimsPrincipal userPrincipal
    )
    {
        try
        {
            // Récupérer l'utilisateur connecté
            var user = CheckUser.GetUserFromClaim(userPrincipal, context);
            if (user == null)
            {
                return new Response<bool>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = false,
                };
            }

            // Vérifier que l'utilisateur est bien un enseignant
            var teacher = await context.Teachers.FirstOrDefaultAsync(t =>
                t.UserId == user.Id
            );

            if (teacher == null)
            {
                return new Response<bool>
                {
                    Status = 403,
                    Message = "Vous devez être un enseignant pour supprimer des créneaux",
                    Data = false,
                };
            }

            var slot = await context
                .Slots.Include(s => s.Reservation)
                .FirstOrDefaultAsync(s => s.Id == slotId && s.ArchivedAt == null);

            if (slot == null)
            {
                return new Response<bool>
                {
                    Status = 404,
                    Message = "Créneau non trouvé",
                    Data = false,
                };
            }

            // Vérifier que le créneau appartient bien à l'enseignant
            if (slot.TeacherId != teacher.Id)
            {
                return new Response<bool>
                {
                    Status = 403,
                    Message = "Vous n'êtes pas autorisé à supprimer ce créneau",
                    Data = false,
                };
            }

            // Vérifier que le créneau n'est pas déjà réservé
            if (slot.Reservation != null)
            {
                return new Response<bool>
                {
                    Status = 400,
                    Message = "Impossible de supprimer un créneau réservé",
                    Data = false,
                };
            }

            slot.ArchivedAt = DateTimeOffset.UtcNow;
            slot.UpdatedAt = DateTimeOffset.UtcNow;

            await context.SaveChangesAsync();

            return new Response<bool>
            {
                Status = 200,
                Message = "Créneau supprimé avec succès",
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new Response<bool>
            {
                Status = 500,
                Message = $"Erreur lors de la suppression du créneau: {ex.Message}",
                Data = false,
            };
        }
    }

    /// <summary>
    /// Récupère les créneaux d'un enseignant entre deux dates
    /// </summary>
    /// <param name="userPrincipal">Principal de l'utilisateur connecté</param>
    /// <param name="dateFrom">Date de début</param>
    /// <param name="dateTo">Date de fin</param>
    /// <returns>Liste des créneaux</returns>
    public async Task<Response<List<SlotDetails>>> GetSlotsByTeacherAndDatesAsync(
        ClaimsPrincipal userPrincipal,
        DateTimeOffset dateFrom,
        DateTimeOffset dateTo
    )
    {
        try
        {
            // Récupérer l'utilisateur connecté
            var user = CheckUser.GetUserFromClaim(userPrincipal, context);
            if (user == null)
            {
                return new Response<List<SlotDetails>>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = null,
                };
            }

            // Vérifier que l'utilisateur est bien un enseignant
            var teacher = await context.Teachers.FirstOrDefaultAsync(t =>
                t.UserId == user.Id
            );

            if (teacher == null)
            {
                return new Response<List<SlotDetails>>
                {
                    Status = 403,
                    Message = "Vous devez être un enseignant pour consulter des créneaux",
                    Data = null,
                };
            }

            // Validation des dates
            if (dateFrom >= dateTo)
            {
                return new Response<List<SlotDetails>>
                {
                    Status = 400,
                    Message = "La date de fin doit être postérieure à la date de début",
                    Data = null,
                };
            }

            var slots = await context
                .Slots.AsNoTracking()
                .Include(s => s.Teacher)
                .ThenInclude(t => t.User)
                .Include(s => s.Type)
                .Where(s =>
                    s.TeacherId == teacher.Id
                    && s.ArchivedAt == null
                    && s.DateFrom >= dateFrom
                    && s.DateTo <= dateTo
                )
                .OrderBy(s => s.DateFrom)
                .Select(s => new SlotDetails(s))
                .ToListAsync();

            return new Response<List<SlotDetails>>
            {
                Status = 200,
                Message = "Créneaux récupérés avec succès",
                Data = slots,
                Count = slots.Count,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<SlotDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des créneaux: {ex.Message}",
                Data = null,
            };
        }
    }

    /// <summary>
    /// Récupère les créneaux d'un enseignant spécifique entre deux dates (pour consultation publique)
    /// </summary>
    /// <param name="teacherId">Identifiant de l'enseignant</param>
    /// <param name="dateFrom">Date de début</param>
    /// <param name="dateTo">Date de fin</param>
    /// <returns>Liste des créneaux disponibles</returns>
    public async Task<Response<List<SlotDetails>>> GetSlotsByTeacherIdAndDatesAsync(
        Guid teacherId,
        DateTimeOffset dateFrom,
        DateTimeOffset dateTo
    )
    {
        try
        {
            // Vérifier que l'enseignant existe
            var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
            {
                return new Response<List<SlotDetails>>
                {
                    Status = 404,
                    Message = "Enseignant non trouvé",
                    Data = null,
                };
            }

            // Validation des dates
            if (dateFrom >= dateTo)
            {
                return new Response<List<SlotDetails>>
                {
                    Status = 400,
                    Message = "La date de fin doit être postérieure à la date de début",
                    Data = null,
                };
            }

            var slots = await context
                .Slots.AsNoTracking()
                .Include(s => s.Teacher)
                .ThenInclude(t => t.User)
                .Include(s => s.Type)
                .Include(s => s.Reservation)
                .Where(s =>
                    s.TeacherId == teacherId
                    && s.ArchivedAt == null
                    && s.DateFrom >= dateFrom
                    && s.DateTo <= dateTo
                    && s.Reservation == null // Seulement les créneaux disponibles
                )
                .OrderBy(s => s.DateFrom)
                .Select(s => new SlotDetails(s))
                .ToListAsync();

            return new Response<List<SlotDetails>>
            {
                Status = 200,
                Message = "Créneaux disponibles récupérés avec succès",
                Data = slots,
                Count = slots.Count,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<SlotDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des créneaux: {ex.Message}",
                Data = null,
            };
        }
    }
}
