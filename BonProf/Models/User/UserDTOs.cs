using SemPaiGo.Models;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class UserDetails : ICreatable
{
    [Required]
    public required Guid Id { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    public string? ImgUrl { get; set; }

    [Required]
    public required string Email { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }

    public GenderDetails? Gender { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    public ICollection<RoleDetails>? Roles { get; set; }

    [Required]
    public ICollection<AddressDetails> Addresses { get; set; }
    [SetsRequiredMembers]
    public UserDetails(UserApp user, List<RoleDetails>? roles,bool minimal = false)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email ?? "";
        Roles = roles;
        Gender = user.Gender is null ? null : new GenderDetails(user.Gender);
        Addresses =  user.Addresses.Select(a => new AddressDetails(a, minimal)).ToList();
        PhoneNumber = user.PhoneNumber;
        DateOfBirth = user.DateOfBirth;
        ImgUrl = user.ImgUrl;
        CreatedAt = user.CreatedAt;
    }
}

/// <summary>
/// Modèle de données pour la connexion utilisateur
/// </summary>
public class UserLogin
{
    /// <summary>
    /// Adresse email de l'utilisateur (format email valide requis)
    /// </summary>
    /// <example>utilisateur@exemple.com</example>
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public required string Email { get; set; }

    /// <summary>
    /// Mot de passe (minimum 8 caractères avec majuscules, minuscules, chiffres)
    /// </summary>
    /// <example>MonMotDePasse123!</example>
    [Required(ErrorMessage = "Le mot de passe est requis")]
    [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
    public required string Password { get; set; }
}

public class ConfirmAccount
{
    public required string UserId { get; set; }
    public required string ConfirmationToken { get; set; }
}

public class UserCreate
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required bool DataProcessingConsent { get; set; } = false;
    [Required]
    public required bool PrivacyPolicyConsent { get; set; } = false;
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public Guid RoleId { get; set; } = HardCode.ROLE_STUDENT;
    [Required]
    public Guid GenderId { get; set; } = HardCode.GENDER_OTHER;
}

public class PasswordResetOutput
{
    [Required]
    public required string ResetToken { get; set; } = string.Empty;

    [Required]
    public required string Email { get; set; } = string.Empty;

    [Required]
    public required Guid Id { get; set; }
}

public class ForgotPassword
{
    [Required(ErrorMessage = "Email required")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}

public class ChangePassword
{
    [Required]
    public required string OldPassword { get; set; }

    [Required]
    public required string NewPassword { get; set; }

    [Required]
    public required string NewPasswordConfirmation { get; set; }
}

public class PasswordRecovery
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

public class LoginOutput
{
    [Required]
    public required string Token { get; set; } = null!;

    [Required]
    public required string RefreshToken { get; set; } = null!;

    [Required]
    public required UserDetails User { get; set; } = null!;
}

public class UserUpdateInput
{
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }

    public void UpdateUser(UserApp user)
    {
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.DateOfBirth = DateOfBirth;
        user.PhoneNumber = PhoneNumber;
    }
}

public class UserInfosWithtoken
{
    [Required]
    public required string Token { get; set; }

    [Required]
    public required UserDetails User { get; set; }
}

public class UserPublicReport
{
    public required int FreeSlotsCount { get; set; }
    public required int GivenBookingsCount { get; set; }
    public required int StudentsCount { get; set; }
}