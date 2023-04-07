using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moja_Aplikacija.Data;
using Moja_Aplikacija.Entity;
using Moja_Aplikacija.Extensions;
using Moja_Aplikacija.Models;

namespace Moja_Aplikacija.Controllers
{ [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index([FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return View(_context.User.ToModel());
            }
            else
            {
                var user = _context.User
                    .Where(p => p.FirstName.ToLower().Contains(filter.ToLower())
                    || p.LastName.ToLower().Contains(filter.ToLower()));
                return View(user.ToModel());
            }
           
        }
        public IActionResult Create()
        {
            var user = new UserModel
            {
               Roles =  _context.Role.Select(p => new RoleModel
                {
                    RoleId = p.RoleId,
                    RoleName = p.Name
                }).OrderBy(p => p.RoleName)
               .ToList()
            };
            return View("Edit", user);
        }
        public IActionResult Edit(int id)
        {
            var user = _context.User
                 .Include(p => p.UserRole)
                 .ThenInclude(p => p.Role)
                 .SingleOrDefault(p => p.UserId == id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var userModel = user.ToModel();
            userModel.Roles.ForEach(p => { p.Selected = true; });

            var roles = _context.Role
                .Where(p => !user.UserRole.Select(p => p.RoleId).Contains(p.RoleId));

            userModel.Roles
                .AddRange(roles.Select(p => new RoleModel
                {
                    RoleId = p.RoleId,
                    RoleName = p.Name
                }));
            userModel.Roles = userModel.Roles
                .OrderBy(p => p.RoleName)
                .ToList();

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userModel);
                }
                else
                {
                    if (userModel.UserId == 0)
                    {
                        userModel.Roles.RemoveAll(p => !p.Selected);
                        var user = userModel.ToModel();
                        _context.User.AddRange(user);
                       
                    }
                    else
                    {
                        var user = userModel.ToModel();
                        _context.Entry(user).State = EntityState.Modified;

                        if (_context.User.SingleOrDefault(p => p.UserId == p.UserId) == null)
                        {
                            return RedirectToAction("Index");
                        }
                        if (string.IsNullOrEmpty(user.Password))
                        {
                            var usr = _context.Entry(user);
                            usr.Property(p => p.Password).IsModified = false;
                        }
                        var currentRoles = _context.UserRole
                            .Where(p => p.UserId == p.UserId)
                            .Select(p => p.RoleId);

                        user.UserRole
                            .Where(p => !currentRoles.Contains(p.RoleId))
                            .ToList()
                            .ForEach(p => _context.Entry(p).State = EntityState.Added);

                        user.UserRole
                            .Where(p => userModel.Roles.Any(p => p.RoleId == p.RoleId && !p.Selected) &&
                            currentRoles.Contains(p.RoleId))
                            .ToList()
                            .ForEach(p => _context.Entry(p).State = EntityState.Deleted);
                        

                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
            
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.User.SingleOrDefault(p => p.UserId == id);
                if (user != null)
                {
                    _context.User.Remove(user);
                    _context.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
         
        }
    }
}
