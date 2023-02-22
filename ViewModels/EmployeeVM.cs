using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC75NET.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC75NET.ViewModels
{
    public class EmployeeVM
    {
        [Required,MaxLength(5), MinLength(0)]
        public string NIK { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name =  "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Gender")]
        public GenderEnum Gender { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; } = DateTime.Now;

        [Display(Name = "Email"), MaxLength(50), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number"), MaxLength(20), Phone]
        public string? PhoneNumber { get; set; }
    }
    public enum GenderEnum
    {
        Male,
        Female
    }
}
