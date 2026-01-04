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
    /// R�cup�re tous les profils enseignants
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
                Message = "Profils enseignants r�cup�r�s avec succ�s",
                //Data = profiles.Select(p => new TeacherDetails(p)).ToList(),
            };
        }
        catch (Exception ex)
        {
            return new Response<List<TeacherDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la r�cup�ration des profils: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// R�cup�re un profil enseignant par son identifiant
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
                    Message = "Profil enseignant non trouv�",
                };
            }
            var teacher = await _context
                .Users
                .Where(p => p.Id == user.Id)
                .Include(p => p.Gender)
                .Include(p => p.Languages)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync();
                

            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouv�",
                };
            }

            

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant r�cup�r� avec succ�s",
                Data = new UserDetails(teacher, null),
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la r�cup�ration du profil: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// R�cup�re un profil enseignant par l'identifiant de l'utilisateur
    /// </summary>
    public async Task<Response<UserDetails>> GetTeacherProfileByUserIdAsync(Guid userId)
    {
        try
        {
            var teacher = await _context
                .Users
                .Where(p => p.Id == userId)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync();
            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouv�",
                };
            }

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant r�cup�r� avec succ�s",
                Data = new UserDetails(teacher, null),
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la r�cup�ration du profil: {ex.Message}",
            };
        }
    }

    /// <summary>
    /// Met � jour le profil enseignant de l'utilisateur connect�
    /// </summary>
    /// <param name="teacherUpdateDto">Donn�es de mise � jour du profil enseignant</param>
    /// <param name="userPrincipal">Principal de l'utilisateur connect�</param>
    /// <returns>Profil enseignant mis � jour</returns>
    public async Task<Response<UserDetails>> UpdateTeacherProfileAsync(
        TeacherUpdate teacherUpdateDto,
        ClaimsPrincipal userPrincipal
    )
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // R�cup�rer l'utilisateur connect�
            var user = CheckUser.GetUserFromClaim(userPrincipal, _context);
            if (user == null)
            {
                return new Response<UserDetails>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifi�",
                    Data = null,
                };
            }

            // V�rifier que l'utilisateur est bien un enseignant
            var teacher = await _context
                .Teachers
                .Include(t => t.User)
                .ThenInclude(p => p.Languages)
                .FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (teacher == null)
            {
                return new Response<UserDetails>
                {
                    Status = 404,
                    Message = "Profil enseignant non trouv�",
                    Data = null,
                };
            }

            // Mettre � jour les informations du profil Teacher
            teacher.Title = teacherUpdateDto.Title;
            teacher.Description = teacherUpdateDto.Description;
            teacher.LinkedIn = teacherUpdateDto.LinkedIn;
            teacher.FaceBook = teacherUpdateDto.FaceBook;
            teacher.GitHub = teacherUpdateDto.GitHub;
            teacher.Twitter = teacherUpdateDto.Twitter;
            teacher.PriceIndicative = teacherUpdateDto.PriceIndicative;
            teacher.UpdatedAt = DateTimeOffset.UtcNow;

            // Mettre � jour le profil de base si fourni
       
                teacher.User.FirstName = teacherUpdateDto.FirstName;
                teacher.User.LastName = teacherUpdateDto.LastName;
                teacher.User.DateOfBirth = teacherUpdateDto.DateOfBirth;
                teacher.User.GenderId = teacherUpdateDto.GenderId;
                teacher.User.UpdatedAt = DateTimeOffset.UtcNow;
          

            // Mettre � jour les langues si fournies
            if (teacherUpdateDto.LanguageIds != null && teacherUpdateDto.LanguageIds.Any())
            {
                // R�cup�rer les langues depuis la base de donn�es
                var languages = await _context
                    .Languages
                    .Where(l => teacherUpdateDto.LanguageIds.Contains(l.Id) && l.ArchivedAt == null)
                    .ToListAsync();

                // V�rifier que toutes les langues existent
                if (languages.Count != teacherUpdateDto.LanguageIds.Count)
                {
                    await transaction.RollbackAsync();
                    return new Response<UserDetails>
                    {
                        Status = 400,
                        Message = "Une ou plusieurs langues sp�cifi�es n'existent pas",
                        Data = null,
                    };
                }

                // Remplacer les langues du profil
                if (teacher.User != null)
                {
                    teacher.User.Languages.Clear();
                    foreach (var language in languages)
                    {
                        teacher.User.Languages.Add(language);
                    }
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Recharger les donn�es compl�tes pour la r�ponse
            var updatedUser = await _context
                .Users
                .Include(p => p.Gender)
                .Include(p => p.Languages)
                .Include(p => p.Formations)
                .Include(p => p.Addresses)
                .Include(u => u.Teacher)
                .ThenInclude(t => t.Cursuses)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return new Response<UserDetails>
            {
                Status = 200,
                Message = "Profil enseignant mis � jour avec succ�s",
                Data = new UserDetails(updatedUser!, null),
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new Response<UserDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise � jour du profil: {ex.Message}",
                Data = null,
            };
        }
    }
}
