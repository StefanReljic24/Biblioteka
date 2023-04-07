using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Moja_Aplikacija.Data;
using Moja_Aplikacija.Models;

namespace Moja_Aplikacija.Controllers
{
    public class LoginController : Controller
    {
        readonly DataContext _context;
        public LoginController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        public IActionResult Index(LoginModel data)
        {
            var user = _context.User
             .Include(p => p.UserRole)
             .ThenInclude(p => p.Role)
             .FirstOrDefault(p => p.Active &&
             p.UserName.ToLower() == data.UserName.ToLower()
             && p.Password == data.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Prijava nije uspela");
                return View(data);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.FirstName + "" + user.LastName),
                new Claim("UserId",user.UserId.ToString())
            };
            foreach (var r in user.UserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, r.Role.Name));
            }
            var claimIndeties = new ClaimsIdentity (claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIndeties);

            Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal
                , new AuthenticationProperties() { IsPersistent = data.RememberMe });

            return Redirect("/");
        }
        public IActionResult Logout()
        {
            Request.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
