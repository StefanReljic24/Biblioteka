using Moja_Aplikacija.Attribute;
using Moja_Aplikacija.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Moja_Aplikacija.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Id je obavezan")]
        public int UserId { get; set; }
        [DisplayName("Ime")]
        [Required(ErrorMessage ="Ime je obavezno")]
        public string FirstName { get; set; } = "";
        [DisplayName("Prezime")]
        [Required(ErrorMessage ="Prezime je obavezno")]
        public string LastName { get; set; } = "";
        [DisplayName("Korisnicko ime")]
        [Required(ErrorMessage ="Korisnicko ime je obavezno")]
        public string UserName { get; set; } = "";
        [DisplayName("Lozinka")]
        [RequiredIf("UserId","0",ErrorMessage ="Lozinka je obavezna")]
        [DataType("Password")]
        public string Password { get; set; } = "";
        [DisplayName("Mejl")]
        [Required(ErrorMessage ="Mejl je obavezan")]
        [EmailAddress(ErrorMessage ="Format mejla nije ispravan")]
        public string Email { get; set; } = "";
        [DisplayName("Aktivan")]
        public bool Active { get; set; }
        [RequiredRole(ErrorMessage ="Korisnik mora da ima minimum jednu ulogu")]
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
        [DisplayName("Potvrda lozinke")]
        [DataType("Password")]
        [Compare("Password",ErrorMessage ="Potvrda lozinke nije tacna")]
        public string? ConfirmPassword { get; set; }

    }
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = "";
        public bool Selected { get; set; }
    }
}
