using Microsoft.AspNetCore.Mvc;
using Moja_Aplikacija.Data;
using Moja_Aplikacija.Entity;

namespace Moja_Aplikacija.Controllers
{
    public class GenreController : Controller
    {
        readonly DataContext _context;
        public GenreController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index([FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return View(_context.Genre.ToList());
            }
            else
            {
                var genre = _context.Genre
                    .Where(p => p.Name.ToLower().Contains(filter.ToLower()));
                return View(genre.ToList());
            }
            
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Genre.SingleOrDefault(p => p.GenreId == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(genre);
                }
                else
                {
                    if (genre.GenreId == 0)
                    {
                        _context.Genre.AddRange(genre);
                    }
                    else
                    {
                        _context.Genre.Update(genre);
                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch 
            {

                return View();
            }
            
        }
        public IActionResult Create()
        {
            return View("Edit", new Genre());
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var genre = _context.Genre.SingleOrDefault(p => p.GenreId == id);
                if (genre != null)
                {
                    _context.Genre.Remove(genre);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch 
            {

                return RedirectToAction("Index");
            }
        }
    }
}
