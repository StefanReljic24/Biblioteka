using Moja_Aplikacija.Entity;

namespace Moja_Aplikacija.Models
{
    public class BookListView
    {
        public IEnumerable<Book> Books { get; set; }
        public string Filter { get; set; } = "";
        public IEnumerable<GenreFilter> GenreFilter { get; set; }
    }
    public class GenreFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }

    }
}
