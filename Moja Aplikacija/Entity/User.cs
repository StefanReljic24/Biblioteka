using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moja_Aplikacija.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string UserName { get; set; } = "";

        public string? Password { get; set; } 

        public string Email { get; set; } = "";

        public bool Active { get; set; }
        

        public List<UserRole> UserRole { get; set; } = new List<UserRole>();
    }
}
