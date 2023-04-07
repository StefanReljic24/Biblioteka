using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moja_Aplikacija.Entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [DisplayName("Naziv")]
        [Required(ErrorMessage ="Naziv je obavezan")]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [DisplayName("Pisac")]
        [Required(ErrorMessage = "Pisac je obavezan")]
        public Writer Writer { get; set; } = new Writer();
        [DisplayName("Godina Izdanja")]
        [Required(ErrorMessage ="Godina izdanja je obavezna")]
        [Range(0,double.PositiveInfinity,ErrorMessage ="Godina izdavanja ne sme da bude manja od 0")]
        public int YearIssued { get; set; }
        [DisplayName("Zanr")]
        [Required(ErrorMessage = "Zanr je obavezan")]
        public Genre Genre { get; set; } = new Genre();
        [DisplayName("Aktivan")]
        public bool Active { get; set; }
        [DisplayName("Slika")]
        public string ImageName { get; set; } = "";
        [NotMapped]
        public string ImagePath => "/images/" + ImageName;
        [NotMapped]
        public IFormFile? NewImage { get; set; }


    }
}
