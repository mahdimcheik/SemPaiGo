using System.ComponentModel.DataAnnotations;
using SemPaiGo.Models;

namespace BonProf.Models;

public class ProfileDetails
{
    [Required]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? LinkedIn { get; set; }
    public string? FaceBook { get; set; }
    public string? GitHub { get; set; }
    public string? Twitter { get; set; }
    public TeacherDetails? Teacher { get; set; }
    public StudentDetails? Student { get; set; }
    public UserDetails? User { get; set; }

    public ICollection<LanguageDetails> Languages { get; set; } = new List<LanguageDetails>();
    public ICollection<AddressDetails> Addresses { get; set; } = new List<AddressDetails>();

    public ProfileDetails(Profile profile)
    {
        Id = profile.Id;
        Title = profile.Title;
        Description = profile.Description;
        LinkedIn = profile.LinkedIn;
        FaceBook = profile.FaceBook;
        GitHub = profile.GitHub;
        Twitter = profile.Twitter;
        Languages = profile.Languages?.Select(l => new LanguageDetails(l))?.ToList() ?? [];
        Addresses = profile.Addresses.Select(a => new AddressDetails(a))?.ToList() ?? [];

        if (profile.Teacher is not null)
        {
            Teacher = new TeacherDetails(profile.Teacher);
        }
        if (profile.Student is not null)
        {
            Student = new StudentDetails(profile.Student);
        }
        if(profile.User is not null)
        {
            User = new UserDetails(profile.User, null, minimal: true);
        }
    }
}
