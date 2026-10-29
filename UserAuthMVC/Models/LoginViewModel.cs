using System.ComponentModel.DataAnnotations;

namespace UserAuthMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email or Username is required.")]
        [Display(Name = "Email or Username")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
