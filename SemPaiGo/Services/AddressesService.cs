using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des adresses
/// </summary>
public class AddressesService(MainContext context)
{
    /// <summary>
    /// Récupère toutes les adresses
    /// </summary>
    /// <returns>Liste des adresses</returns>
    public async Task<Response<List<AddressDetails>>> GetAllAddressesAsync()
    {
        try
        {
            var addresses = await context.Addresses
                .AsNoTracking()
                .Where(a => a.ArchivedAt == null)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AddressDetails(a))
                .ToListAsync();

            return new Response<List<AddressDetails>>
            {
                Status = 200,
                Message = "Adresses récupérées avec succès",
                Data = addresses,
                Count = addresses.Count
            };
        }
        catch (Exception ex)
        {
            return new Response<List<AddressDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des adresses: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Récupère une adresse par son identifiant
    /// </summary>
    /// <param name="id">Identifiant de l'adresse</param>
    /// <returns>Adresse trouvée</returns>
    public async Task<Response<AddressDetails>> GetAddressByIdAsync(Guid id)
    {
        try
        {
            var address = await context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.ArchivedAt == null);

            if (address == null)
            {
                return new Response<AddressDetails>
                {
                    Status = 404,
                    Message = "Adresse non trouvée",
                    Data = null
                };
            }

            return new Response<AddressDetails>
            {
                Status = 200,
                Message = "Adresse récupérée avec succès",
                Data = new AddressDetails(address)
            };
        }
        catch (Exception ex)
        {
            return new Response<AddressDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération de l'adresse: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Récupère les adresses d'un utilisateur
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur</param>
    /// <returns>Liste des adresses de l'utilisateur</returns>
    public async Task<Response<List<AddressDetails>>> GetAddressesByUserIdAsync(Guid userId)
    {
        try
        {
            var addresses = await context.Addresses
                .AsNoTracking()
                .Where(a => a.UserId == userId && a.ArchivedAt == null)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AddressDetails(a))
                .ToListAsync();

            return new Response<List<AddressDetails>>
            {
                Status = 200,
                Message = "Adresses de l'utilisateur récupérées avec succès",
                Data = addresses,
                Count = addresses.Count
            };
        }
        catch (Exception ex)
        {
            return new Response<List<AddressDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des adresses de l'utilisateur: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Crée une nouvelle adresse
    /// </summary>
    /// <param name="addressDto">Données de l'adresse à créer</param>
    /// <returns>Adresse créée</returns>
    public async Task<Response<AddressDetails>> CreateAddressAsync(AddressCreate addressDto)
    {
        try
        {
            // Vérifier que l'utilisateur existe
            var userExists = await context.Users.AnyAsync(u => u.Id == addressDto.UserId );
            if (!userExists)
            {
                return new Response<AddressDetails>
                {
                    Status = 404,
                    Message = "Utilisateur non trouvé",
                    Data = null
                };
            }

            var address = new Address(addressDto);
           

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            return new Response<AddressDetails>
            {
                Status = 201,
                Message = "Adresse créée avec succès",
                Data = new AddressDetails(address)
            };
        }
        catch (Exception ex)
        {
            return new Response<AddressDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création de l'adresse: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Met à jour une adresse existante
    /// </summary>
    /// <param name="id">Identifiant de l'adresse</param>
    /// <param name="addressDto">Nouvelles données de l'adresse</param>
    /// <returns>Adresse mise à jour</returns>
    public async Task<Response<AddressDetails>> UpdateAddressAsync(AddressUpdate addressDto)
    {
        try
        {
            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == addressDto.Id && a.ArchivedAt == null);

            if (address == null)
            {
                return new Response<AddressDetails>
                {
                    Status = 404,
                    Message = "Adresse non trouvée",
                    Data = null
                };
            }

            // Vérifier que l'utilisateur existe
            var userExists = await context.Users.AnyAsync(u => u.Id == addressDto.UserId);
            if (!userExists)
            {
                return new Response<AddressDetails>
                {
                    Status = 404,
                    Message = "Utilisateur non trouvé",
                    Data = null
                };
            }
            address.UpdateAddress(addressDto);

            await context.SaveChangesAsync();

            return new Response<AddressDetails>
            {
                Status = 200,
                Message = "Adresse mise à jour avec succès",
                Data = new AddressDetails(address)
            };
        }
        catch (Exception ex)
        {
            return new Response<AddressDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour de l'adresse: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Archive une adresse (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant de l'adresse</param>
    /// <returns>Résultat de l'opération</returns>
    public async Task<Response<object>> DeleteAddressAsync(Guid id)
    {
        try
        {
            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id && a.ArchivedAt == null);

            if (address == null)
            {
                return new Response<object>
                {
                    Status = 404,
                    Message = "Adresse non trouvée",
                    Data = null
                };
            }

            address.ArchivedAt = DateTimeOffset.UtcNow;
            address.UpdatedAt = DateTimeOffset.UtcNow;

            await context.SaveChangesAsync();

            return new Response<object>
            {
                Status = 200,
                Message = "Adresse supprimée avec succès",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new Response<object>
            {
                Status = 500,
                Message = $"Erreur lors de la suppression de l'adresse: {ex.Message}",
                Data = null
            };
        }
    }
}
