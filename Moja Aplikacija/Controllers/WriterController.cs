using Microsoft.AspNetCore.Mvc;
using Moja_Aplikacija.Data;
using Moja_Aplikacija.Entity;

namespace Moja_Aplikacija.Controllers
{
    public class WriterController : Controller
    {
        readonly DataContext _context;
        readonly string _ImagePath;
        public WriterController(DataContext context,IWebHostEnvironment env)
        {
            _context = context;
            _ImagePath = env.ContentRootPath + @"/wwwroot/images/";
        }
        public IActionResult Index([FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return View(_context.Writer.ToList());
            }
            else
            {
                var writer = _context.Writer
                    .Where(p => p.FirstName.ToLower().Contains(filter.ToLower())
                    || p.LastName.ToLower().Contains(filter.ToLower()));
                return View(writer.ToList());
            }
           
        }
        public IActionResult Edit(int id) 
        {
            return View(_context.Writer.SingleOrDefault(p => p.WriterId == id));
        }
        public IActionResult Create()
        {
            return View("Edit", new Writer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Writer writer)
        {
            if (!ModelState.IsValid)
            {
                return View(_context.Writer.ToList());
            }
            else
            {
                if (writer.WriterId == 0)
                {
                    _context.Writer.AddRange(writer);
                }
                else
                {
                    _context.Writer.Update(writer);
                }
              
            }
            if (writer.NewImage != null)
            {
                if (writer.NewImage.Length > 0)
                {
                    using (var stream = System.IO.File.Create(_ImagePath + writer.NewImage.FileName))
                    {
                        writer.NewImage.CopyTo(stream);
                    }

                }
                DeleteImage(writer.ImageName);
                writer.ImageName = writer.NewImage.FileName;

            }
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            try
            {
                var writer = _context.Writer.SingleOrDefault(p => p.WriterId == id);
                if (writer != null)
                {
                    _context.Writer.Remove(writer);
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
            if (string.IsNullOrEmpty(imageName))
            {
                return;
            }
            var FullPathImage = _ImagePath + imageName;
            if (System.IO.File.Exists(FullPathImage) && !imageName.EndsWith(".jfif"))
            {
                System.IO.File.Delete(FullPathImage);
            } 
            
        }
    }
}
