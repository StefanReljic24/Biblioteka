using Microsoft.AspNetCore.Mvc;
using Moja_Aplikacija.Data;

namespace Moja_Aplikacija.Controllers
{
    public class DashboardController : Controller
    {
        DataContext _context;
        public DashboardController(DataContext context)
        {
            _context = context;
        }
        public IActionResult TopBooks()
        {
            var book = _context.Book
                .OrderBy(p => Guid.NewGuid())
                .Take(3);
            return PartialView(book);
        }

        //public IActionResult Counters()
       // {
          //  ViewData["userCount"] = _context.User.Count();
           // ViewData["bookCount"] = _context.Book.Count();

           // return PartialView();
        //}
    }
}
