using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des genres
/// </summary>
public class GendersService(MainContext context)
{
    /// <summary>
    /// Récupère tous les genres
    /// </summary>
    /// <returns>Liste des genres</returns>
    public async Task<Response<List<GenderDetails>>> GetAllGendersAsync()
    {
        try
        {
            var genders = await context
                .Genders.AsNoTracking()
                .Where(g => g.ArchivedAt == null)
                .OrderBy(g => g.Name)
                .Select(g => new GenderDetails(g))
                .ToListAsync();

            return new Response<List<GenderDetails>>
            {
                Status = 200,
                Message = "Genres récupérés avec succès",
                Data = genders,
                Count = genders.Count,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<GenderDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des genres: {ex.Message}",
                Data = null,
            };
        }
    }
}
