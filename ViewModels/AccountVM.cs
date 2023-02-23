using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC75NET.ViewModels
{
    public class AccountVM
    {
        [Display(Name ="Employee NIK")]
        public string EmployeeNIK { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
