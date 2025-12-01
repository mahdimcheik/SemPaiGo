using Microsoft.AspNetCore.Identity;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class UserApp : IdentityUser<Guid>, IArchivable, IUpdateable, ICreatable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public bool DataProcessingConsent { get; set; } = false;
    public bool PrivacyPolicyConsent { get; set; } = false;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImgUrl { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    // roles
    public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

    //gender
    public Guid GenderId { get; set; }
    public Gender? Gender { get; set; }

    // Status account
    public Guid StatusId { get; set; }
}

public class UserResponseDTO : ICreatable
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImgUrl { get; set; }

    public GenderDTO? Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    public ICollection<RoleAppResponseDTO> Roles { get; set; }

    public UserResponseDTO(UserApp user, List<RoleAppResponseDTO>? roles)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Roles = roles;
        Gender = user.Gender is null ? null : new GenderDTO(user.Gender);
        Title = user.Title;
        Description = user.Description;
        PhoneNumber = user.PhoneNumber;
        DateOfBirth = user.DateOfBirth;
        ImgUrl = user.ImgUrl;
        CreatedAt = user.CreatedAt;
    }
}

/// <summary>
/// Modèle de données pour la connexion utilisateur
/// </summary>
public class UserLoginDTO
{
    /// <summary>
    /// Adresse email de l'utilisateur (format email valide requis)
    /// </summary>
    /// <example>utilisateur@exemple.com</example>
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; }

    /// <summary>
    /// Mot de passe (minimum 8 caractères avec majuscules, minuscules, chiffres)
    /// </summary>
    /// <example>MonMotDePasse123!</example>
    [Required(ErrorMessage = "Le mot de passe est requis")]
    [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
    public string Password { get; set; }
}

public class ConfirmAccountInput
{
    public string UserId { get; set; }
    public string ConfirmationToken { get; set; }
}

public class UserCreateDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public bool DataProcessingConsent { get; set; } = false;
    [Required]
    public bool PrivacyPolicyConsent { get; set; } = false;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }

    public DateTimeOffset DateOfBirth { get; set; }
    public Guid? RoleId { get; set; }
    public Guid GenderId { get; set; } = HardCode.GENDER_OTHER;

    public UserApp ToUser()
    {
        return new UserApp
        {
            UserName = Email,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            DateOfBirth = DateOfBirth,
            Title = Title,
            GenderId = GenderId,
            Description = Description,
            PhoneNumber = PhoneNumber,

            DataProcessingConsent = DataProcessingConsent,
            PrivacyPolicyConsent = PrivacyPolicyConsent
        };
    }
}

public class PasswordResetResponseDTO
{
    [Required]
    public required string ResetToken { get; set; } = string.Empty;

    [Required]
    public required string Email { get; set; } = string.Empty;

    [Required]
    public required Guid Id { get; set; }
}

public class ForgotPasswordInput
{
    [Required(ErrorMessage = "Email required")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}

public class ChangePasswordInput
{
    [Required]
    public required string OldPassword { get; set; }

    [Required]
    public required string NewPassword { get; set; }

    [Required]
    public required string NewPasswordConfirmation { get; set; }
}

public class PasswordRecoveryInput
{
    [Required(ErrorMessage = "UserId required")]
    public required string UserId { get; set; }

    [Required(ErrorMessage = "ConfirmationToken required")]
    public required string ResetToken { get; set; }

    [Required(ErrorMessage = "Password required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "PasswordConfirmation required")]
    public required string PasswordConfirmation { get; set; }
}

public class LoginOutputDTO
{
    [Required]
    public required string Token { get; set; } = null!;

    [Required]
    public required string RefreshToken { get; set; } = null!;

    [Required]
    public required UserResponseDTO User { get; set; } = null!;
}

public class UserUpdateDTO
{
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }

    public List<Guid> LanguagesIds { get; set; } = new();
    public List<Guid> ProgrammingLanguagesIds { get; set; } = new();

    public void UpdateUser(UserApp user)
    {
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.DateOfBirth = DateOfBirth;
        user.Title = Title;
        user.Description = Description;
        user.PhoneNumber = PhoneNumber;
    }
}

public class UserInfosWithtoken
{
    [Required]
    public required string Token { get; set; }

    [Required]
    public required UserResponseDTO User { get; set; }
}

public class UserPublicReport
{
    public int FreeSlotsCount { get; set; }
    public int GivenBookingsCount { get; set; }
    public int StudentsCount { get; set; }
}

