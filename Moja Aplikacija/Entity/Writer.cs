using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moja_Aplikacija.Entity
{
    public class Writer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WriterId { get; set; }
        [DisplayName("Ime")]
        [MaxLength(100)]
        public string FirstName { get; set; } = "";
        [DisplayName("Prezime")]
        [MaxLength(100)]
        public string LastName { get; set; } = "";
        [DisplayName("Slika")]
        public string ImageName { get; set; } = "";
        [NotMapped]
        public string ImagePath => "/images/" + ImageName;
        [NotMapped]
        public IFormFile? NewImage { get; set; }
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

    }
}
