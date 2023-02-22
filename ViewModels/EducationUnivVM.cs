using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC75NET.ViewModels
{
    public class EducationUnivVM
    {
        public int Id { get; set; }
        public string Major { get; set; }


        [Required,MaxLength(2), MinLength(0, ErrorMessage = "Contoh inputan : S1/D3")]
        public string Degree { get; set; }


        [Range(0, 4, ErrorMessage = "Inputan harus lebih dari {1} dan kurang dari {2}")]
        public float Gpa { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }
    }
}
