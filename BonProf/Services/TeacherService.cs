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
                .ThenInclude(p => p.Gender)
                .Include(p => p.Profile)
                .ThenInclude(p => p.Languages)
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

    /// <summary>
    /// Met à jour le profil enseignant de l'utilisateur connecté
    /// </summary>
    /// <param name="teacherUpdateDto">Données de mise à jour du profil enseignant</param>
    /// <param name="userPrincipal">Principal de l'utilisateur connecté</param>
    /// <returns>Profil enseignant mis à jour</returns>
    public async Task<Response<UserDetails>> UpdateTeacherProfileAsync(
        TeacherUpdate teacherUpdateDto,
        ClaimsPrincipal userPrincipal
    )
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Récupérer l'utilisateur connecté
            var user = CheckUser.GetUserFromClaim(userPrincipal, _context);
            if (user == null)
            {
                return new Response<UserDetails>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = null,
                };
            }

            // Vérifier que l'utilisateur est bien un enseignant
            var teacher = await _context
                .Teachers
                .Include(t => t.User)
                .ThenInclude(u => u.Profile)
                .ThenInclude(p => p.Languages)
                .FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouvé",
                    Data = null,
                };
            }

            // Mettre à jour les informations du profil Teacher
            teacher.Title = teacherUpdateDto.Title;
            teacher.Description = teacherUpdateDto.Description;
            teacher.LinkedIn = teacherUpdateDto.LinkedIn;
            teacher.FaceBook = teacherUpdateDto.FaceBook;
            teacher.GitHub = teacherUpdateDto.GitHub;
            teacher.Twitter = teacherUpdateDto.Twitter;
            teacher.PriceIndicative = teacherUpdateDto.PriceIndicative;
            teacher.UpdatedAt = DateTimeOffset.UtcNow;

            // Mettre à jour le profil de base si fourni
            if (teacherUpdateDto.Profile != null && teacher.User?.Profile != null)
            {
                teacher.User.Profile.FirstName = teacherUpdateDto.Profile.FirstName;
                teacher.User.Profile.LastName = teacherUpdateDto.Profile.LastName;
                teacher.User.Profile.DateOfBirth = teacherUpdateDto.Profile.DateOfBirth;
                teacher.User.Profile.GenderId = teacherUpdateDto.Profile.GenderId;
                teacher.User.Profile.UpdatedAt = DateTimeOffset.UtcNow;
            }

            // Mettre à jour les langues si fournies
            if (teacherUpdateDto.LanguageIds != null && teacherUpdateDto.LanguageIds.Any())
            {
                // Récupérer les langues depuis la base de données
                var languages = await _context
                    .Languages
                    .Where(l => teacherUpdateDto.LanguageIds.Contains(l.Id) && l.ArchivedAt == null)
                    .ToListAsync();

                // Vérifier que toutes les langues existent
                if (languages.Count != teacherUpdateDto.LanguageIds.Count)
                {
                    await transaction.RollbackAsync();
                    return new Response<UserDetails>
                    {
                        Status = 400,
                        Message = "Une ou plusieurs langues spécifiées n'existent pas",
                        Data = null,
                    };
                }

                // Remplacer les langues du profil
                if (teacher.User?.Profile != null)
                {
                    teacher.User.Profile.Languages.Clear();
                    foreach (var language in languages)
                    {
                        teacher.User.Profile.Languages.Add(language);
                    }
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Recharger les données complètes pour la réponse
            var updatedUser = await _context
                .Users
                .Include(u => u.Profile)
                .ThenInclude(p => p.Gender)
                .Include(u => u.Profile)
                .ThenInclude(p => p.Languages)
                .Include(u => u.Profile)
                .ThenInclude(p => p.Formations)
                .Include(u => u.Profile)
                .ThenInclude(p => p.Addresses)
                .Include(u => u.Teacher)
                .ThenInclude(t => t.Cursuses)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant mis à jour avec succès",
                Data = new UserDetails(updatedUser!, null),
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour du profil: {ex.Message}",
                Data = null,
            };
        }
    }
}
