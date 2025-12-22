using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des rôles
/// </summary>
public class RolesService(MainContext context)
{
    /// <summary>
    /// Récupère tous les rôles
    /// </summary>
    /// <returns>Liste des rôles</returns>
    public async Task<Response<List<RoleDetails>>> GetAllRolesAsync()
    {
        try
        {
            var roles = await context
                .Roles
                .AsNoTracking()
                .Where(r => r.ArchivedAt == null)
                .OrderBy(r => r.Name)
                .Select(r => new RoleDetails(r))
                .ToListAsync();

            return new Response<List<RoleDetails>>
            {
                Status = 200,
                Message = "Rôles récupérés avec succès",
                Data = roles,
                Count = roles.Count,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<RoleDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des rôles: {ex.Message}",
                Data = null,
            };
        }
    }
}
