using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moja_Aplikacija.Data;
using Moja_Aplikacija.Entity;
using Moja_Aplikacija.Models;

namespace Moja_Aplikacija.Controllers
{
    public class BookController : Controller
    {
        readonly DataContext _context;
        readonly string _ImagePath; 
        public BookController(DataContext context,IWebHostEnvironment env)
        {
            _context = context;
            _ImagePath = env.ContentRootPath + @"/wwwroot/images";
        }
        public IActionResult Index([FromQuery] string filter,[FromQuery] int[] genres)
        {
            if (filter == null)
            {
                filter = "";
            }
            var genre = _context.Genre.ToList();
            IEnumerable<Book> books;
            
            if (string.IsNullOrEmpty(filter) && !genres.Any())
            {
                books = _context.Book.ToList();
            }
            else
            {
                books = _context.Book
                .Where(p => p.Name.ToLower().Contains(filter.ToLower()));

                books = books
                    .Where(p => !genres.Any()
                    || (p.Genre != null && genres.Contains(p.Genre.GenreId)));
            }

            var genreFilter = genre.Select(p => new GenreFilter
            {
                Id = p.GenreId,
                Name = p.Name,
                Selected = genres.Contains(p.GenreId)
            }) ;
            var ViewModel = new BookListView
            {
                Books = books,
                Filter = filter,
                GenreFilter = genreFilter
                
            };
            return View(ViewModel);
            
        }
        public IActionResult Edit(int id)
        {
            var genres = _context.Genre.ToList();
            ViewData["genres"] = new SelectList(genres, "GenreId", "Name");
            return View(_context.Book.SingleOrDefault(p => p.BookId == id));

        }
        public IActionResult Create()
        {
            var genres = _context.Genre.ToList();
            ViewData["genres"] = new SelectList(genres, "GenreId", "Name");
            return View("Edit", new Book());
        }
        public IActionResult Details(int id)
        {
            var book = _context.Book.Include(p => p.Genre)
                 .FirstOrDefault(p => p.BookId == id);
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            try
            {
                ModelState.Remove("Genre.Name");
                ModelState.Remove("Write.Name");
                var genres = _context.Genre.ToList();
                ViewData["genres"] = new SelectList(genres, "GenreId", "Name");

                if (!ModelState.IsValid)
                {
                    return View(_context.Book.ToList());
                }
                else
                {
                    if (book.BookId == 0)
                    {
                        book.Genre = _context.Genre.Single(p => p.GenreId == book.Genre.GenreId);
                        book.Writer = _context.Writer.Single(p => p.WriterId == book.Writer.WriterId);
                        _context.Book.Add(book);
                    }
                    else
                    {
                        book.Genre = null;
                        book.Writer = null;
                        _context.Book.Update(book);
                    }

 
                }
                if (book.NewImage != null)
                {
                    if (book.NewImage.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(_ImagePath + book.NewImage.FileName))
                        {
                            book.NewImage.CopyTo(stream);
                        }
                    }
                    DeleteImage(book.ImageName);
                    book.ImageName = book.NewImage.FileName;

                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {
                
                return View(book);

            }
            
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var book = _context.Book.SingleOrDefault(p => p.BookId == id);
                if (book != null)
                {
                    _context.Book.Remove(book);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch 
            {

                return RedirectToAction("Index");
            }
           
        }
        public void DeleteImage(string imageName)
        {
            var FullPathImage = _ImagePath + imageName;
            if (System.IO.File.Exists(FullPathImage) && imageName.EndsWith(".jfif"))
            {
                System.IO.File.Delete(FullPathImage);
            }
            
        }
    }
}
