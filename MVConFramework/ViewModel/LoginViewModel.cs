using System.ComponentModel.DataAnnotations;

namespace MVConFramework.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username or email")]
        [Display(Name = "Username or Email")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
