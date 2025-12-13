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
public class TeacherProfileService
{
    private readonly MainContext _context;
    private readonly UserManager<UserApp> _userManager;

    public TeacherProfileService(MainContext context, UserManager<UserApp> userManager)
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
                .ProfileTeachers.Include(p => p.User)
                .ThenInclude(u => u.Gender)
                .Include(p => p.User)
                .ThenInclude(u => u.Addresses)
                .Include(p => p.Formations)
                .ToListAsync();

            return new Response<List<TeacherDetails>>
            {
                Status = 200,
                Message = "Profils enseignants récupérés avec succès",
                Data = profiles.Select(p => new TeacherDetails(p)).ToList(),
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
    public async Task<Response<TeacherDetails>> GetTeacherFullProfileAsync(ClaimsPrincipal User)
    {
        try
        {
            var user = CheckUser.GetUserFromClaim(User, _context);
            if (user is null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }
            var profile = await _context
                .ProfileTeachers.Include(p => p.User)
                .ThenInclude(u => u.Gender)
                .Include(p => p.User)
                .ThenInclude(u => u.Addresses.Where(a =>a.ArchivedAt == null))
                .Include(u => u.Languages)
                .Include(p => p.Formations.Where(a => a.ArchivedAt == null))
                .FirstOrDefaultAsync(p => p.Id == user.Id);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }

            if(profile.User?.Addresses is not null)
            {
                var workAddress = profile.User?.Addresses.FirstOrDefault(a => a.TypeId == HardCode.TYPE_ADDRESS_BILLING);
                if(workAddress is not null)
                {
                    profile.User.Addresses = [workAddress];
                }
            }

            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new TeacherDetails(profile),
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// Récupère un profil enseignant par l'identifiant de l'utilisateur
    /// </summary>
    public async Task<Response<TeacherDetails>> GetTeacherProfileByUserIdAsync(Guid userId)
    {
        try
        {
            var profile = await _context
                .ProfileTeachers.Include(p => p.User)
                .ThenInclude(u => u.Gender)
                .Include(p => p.User)
                .ThenInclude(u => u.Addresses)
                .Include(p => p.Formations)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }

            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new TeacherDetails(profile),
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// Met à jour un profil enseignant existant
    /// </summary>
    public async Task<Response<TeacherDetails>> UpdateTeacherProfileAsync(
        TeacherProfileUpdate profileDto,
        ClaimsPrincipal userPrincipal
    )
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var profile = await _context
                .ProfileTeachers.Include(p => p.User)
                .ThenInclude(u => u.Gender)
                .Include(u => u.Languages)
                .FirstOrDefaultAsync(p => p.Id == profileDto.Id);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                };
            }

            // Update profile
            profileDto.UpdateProfile(profile);
            profile.Languages.Clear();
            // Add new languages
            if (profileDto.LanguagesIds?.Any() == true)
            {
                var newLanguages = await _context
                    .Languages.Where(l => profileDto.LanguagesIds.Contains(l.Id))
                    .ToListAsync();

                foreach (var language in newLanguages)
                {
                    profile.Languages.Add(language);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new TeacherDetails(profile),
            };

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour du profil: {ex.Message}",
            };
        }
    }
}
