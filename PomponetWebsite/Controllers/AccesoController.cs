using Microsoft.AspNetCore.Mvc;
using PomponetWebsite.Context;
using PomponetWebsite.Models;
using Microsoft.EntityFrameworkCore;
using PomponetWebsite.ViewsModels;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PomponetWebsite.Controllers
{
    public class AccesoController : Controller
    {
        private readonly CropsDbContext _cropsDbContext;
        public AccesoController(CropsDbContext cropsDbContext)
        {
            _cropsDbContext = cropsDbContext;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrarse(PeopleVM modelo)
        {
            if (modelo.Password != modelo.ConfirmPassword)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            People people = new People()
            {
                Names = modelo.Names,
                Email = modelo.Email,
                UserName = modelo.UserName,
                Age = modelo.Age,
                Password = modelo.Password
            };
            await _cropsDbContext.AddAsync(people);
            await _cropsDbContext.SaveChangesAsync();

            if (people.Id_Person != 0) return RedirectToAction("Login","Acceso");

            ViewData["Mensaje"] = "No se puede crear el usuario";

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated) return RedirectToAction("Index","Home");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            People? people_encontrado = await _cropsDbContext.People
                .Where(p =>
                    p.UserName == modelo.UserName &&
                    p.Password == modelo.Password
                ).FirstOrDefaultAsync();

            if (people_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, people_encontrado.Names)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

			return RedirectToAction("Index","Home");
		}
	}
}
