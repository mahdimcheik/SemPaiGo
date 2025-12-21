using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Utilities;
using System;
using System.Security.Claims;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des profils enseignants
/// </summary>
public class TeacherService
{
    private readonly MainContext _context;
    private readonly UserManager<UserApp> _userManager;

    public TeacherService(MainContext context, UserManager<UserApp> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Récupère tous les profils enseignants
    /// </summary>
    public async Task<Response<List<TeacherDetails>>> GetAllTeacherProfilesAsync()
    {
        try
        {
            var profiles = await _context
                .Teachers.Include(p => p.User)
                .Include(p => p.User)
                .ToListAsync();

            return new Response<List<TeacherDetails>>
            {
                Status = 200,
                Message = "Profils enseignants récupérés avec succès",
                //Data = profiles.Select(p => new TeacherDetails(p)).ToList(),
            };
        }
        catch (Exception ex)
        {
            return new Response<List<TeacherDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des profils: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// Récupère un profil enseignant par son identifiant
    /// </summary>
    public async Task<Response<UserDetails>> GetTeacherFullProfileAsync(ClaimsPrincipal User)
    {
        try
        {
            var user = CheckUser.GetUserFromClaim(User, _context);
            if (user is null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }
            var teacher = await _context
                .Users
                .Where(p => p.Id == user.Id)
                .Include(p => p.Profile)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync();
                

            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }

            

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new UserDetails(teacher, null),
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// Récupère un profil enseignant par l'identifiant de l'utilisateur
    /// </summary>
    public async Task<Response<UserDetails>> GetTeacherProfileByUserIdAsync(Guid userId)
    {
        try
        {
            var teacher = await _context
                .Users
                .Where(p => p.Id == userId)
                .Include(p => p.Profile)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync();
            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new UserDetails(teacher, null),
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}",
            };
        }
    }
}
