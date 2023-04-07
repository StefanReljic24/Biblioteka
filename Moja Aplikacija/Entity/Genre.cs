using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moja_Aplikacija.Entity
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        [DisplayName("Naziv")]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }

}
