using System.ComponentModel.DataAnnotations;
using BonProf.Models;

namespace SemPaiGo.Models;

public class StudentDetails
{
    [Required]
    public Guid Id { get; set; }
    public StudentDetails() { }
    public StudentDetails(Student student) { }
}
