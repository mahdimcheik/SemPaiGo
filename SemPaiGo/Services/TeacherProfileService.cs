using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Utilities;
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
            var profiles = await _context.ProfileTeachers
                .Include(p => p.User)
                    .ThenInclude(u => u.Gender)
                .Include(p => p.User)
                    .ThenInclude(u => u.Addresses)
                .Include(p => p.Formations)
                .ToListAsync();

    

            return new Response<List<TeacherDetails>>
            {
                Status = 200,
                Message = "Profils enseignants récupérés avec succès",
                Data = profiles.Select(p => new TeacherDetails(p)).ToList()
            };
        }
        catch (Exception ex)
        {
            return new Response<List<TeacherDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des profils: {ex.Message}"
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
            if(user is null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé"
                };
            }
            var profile = await _context.ProfileTeachers
                           .Include(p => p.User)
                    .ThenInclude(u => u.Gender)
                .Include(p => p.User)
                    .ThenInclude(u => u.Addresses)
                .Include(p => p.Formations)
                .FirstOrDefaultAsync(p => p.Id == user.Id);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé"
                };
            }        

            

            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = new TeacherDetails(profile)
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}"
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
            var profile = await _context.ProfileTeachers
                .Include(p => p.User)
                    .ThenInclude(u => u.Gender)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé pour cet utilisateur"
                };
            }

            var userRoles = await _userManager.GetRolesAsync(profile.User);
            var roles = await _context.Roles.ToListAsync();
            var rolesDetailed = roles
                .Where(r => userRoles.Contains(r.Name ?? string.Empty))
                .Select(r => new RoleDetails(r))
                .ToList();

            var userDetails = new UserDetails(profile.User, rolesDetailed);

            // Get addresses for this teacher
            var addresses = await _context.Addresses
                .Where(a => a.UserId == profile.UserId)
                .Select(a => new AddressDetails(a))
                .ToListAsync();

            // Get formations for this teacher
            var formations = await _context.Formations
                .Where(f => f.TeacherId == profile.Id)
                .Select(f => new FormationDetails(f))
                .ToListAsync();

            var teacherDetails = new TeacherDetails(profile, userDetails, addresses, formations);

            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant récupéré avec succès",
                Data = teacherDetails
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du profil: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Crée un nouveau profil enseignant
    /// </summary>
    public async Task<Response<TeacherDetails>> CreateTeacherProfileAsync(TeacherProfileCreate profileDto, ClaimsPrincipal userPrincipal)
    {
        try
        {
            // Verify user exists
            var user = await _context.Users
                .Include(u => u.Gender)
                .FirstOrDefaultAsync(u => u.Id == profileDto.UserId);

            if (user == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Utilisateur non trouvé"
                };
            }

            // Check if profile already exists
            var existingProfile = await _context.ProfileTeachers
                .FirstOrDefaultAsync(p => p.UserId == profileDto.UserId);

            if (existingProfile != null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 400,
                    Message = "Un profil enseignant existe déjà pour cet utilisateur"
                };
            }

            var newProfile = new ProfileTeacher
            {
                Id = Guid.NewGuid(),
                UserId = profileDto.UserId,
                Title = profileDto.Title,
                Description = profileDto.Description,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.ProfileTeachers.AddAsync(newProfile);
            await _context.SaveChangesAsync();

            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _context.Roles.ToListAsync();
            var rolesDetailed = roles
                .Where(r => userRoles.Contains(r.Name ?? string.Empty))
                .Select(r => new RoleDetails(r))
                .ToList();

            var userDetails = new UserDetails(user, rolesDetailed);
            var teacherDetails = new TeacherDetails(newProfile, userDetails);

            return new Response<TeacherDetails>
            {
                Status = 201,
                Message = "Profil enseignant créé avec succès",
                Data = teacherDetails
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création du profil: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Met à jour un profil enseignant existant
    /// </summary>
    public async Task<Response<TeacherDetails>> UpdateTeacherProfileAsync(TeacherProfileUpdate profileDto, ClaimsPrincipal userPrincipal)
    {
        try
        {
            var profile = await _context.ProfileTeachers
                .Include(p => p.User)
                    .ThenInclude(u => u.Gender)
                .FirstOrDefaultAsync(p => p.Id == profileDto.Id);

            if (profile == null)
            {
                return new Response<TeacherDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé"
                };
            }

            // Update profile
            profileDto.UpdateProfile(profile);
            await _context.SaveChangesAsync();

            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(profile.User);
            var roles = await _context.Roles.ToListAsync();
            var rolesDetailed = roles
                .Where(r => userRoles.Contains(r.Name ?? string.Empty))
                .Select(r => new RoleDetails(r))
                .ToList();

            var userDetails = new UserDetails(profile.User, rolesDetailed);

            // Get addresses for this teacher
            var addresses = await _context.Addresses
                .Where(a => a.UserId == profile.UserId)
                .Select(a => new AddressDetails(a))
                .ToListAsync();

            // Get formations for this teacher
            var formations = await _context.Formations
                .Where(f => f.TeacherId == profile.Id)
                .Select(f => new FormationDetails(f))
                .ToListAsync();

            var teacherDetails = new TeacherDetails(profile, userDetails, addresses, formations);

            return new Response<TeacherDetails>
            {
                Status = 200,
                Message = "Profil enseignant mis à jour avec succès",
                Data = teacherDetails
            };
        }
        catch (Exception ex)
        {
            return new Response<TeacherDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour du profil: {ex.Message}"
            };
        }
    }
}
