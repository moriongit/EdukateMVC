using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Edukate.ViewModels.AuthVM
{
    public class RegisterVM
    {
        [Required, MaxLength(25),MinLength(3)]
        public string Name { get; set; }
        [Required, MaxLength(25), MinLength(3)]
        public string Surname { get; set; }
        [Required, MaxLength(32), MinLength(3)]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(8), NotNull]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password),Compare("Password", ErrorMessage= "Passwords do not match")]
        public string ConfirmPassword { get; set; }


    }
}
