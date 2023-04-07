using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Moja_Aplikacija.Models
{
    public class LoginModel
    {
        [DisplayName("Korisnicko ime")]
        [Required(ErrorMessage = "Korisnicko ime je obavezno")]
        public string UserName { get; set; } = "";


        [DisplayName("Lozinka")]
        [DataType("Password")]
        [Required(ErrorMessage = "Lozinka je obavezno")]
        public string Password { get; set; } = "";

        [DisplayName("Zapamti me")]
        public bool RememberMe { get; set; }
    }
}
