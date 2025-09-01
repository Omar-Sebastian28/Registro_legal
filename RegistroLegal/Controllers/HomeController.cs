using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmUser;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    //[Authorize (Roles = "Usuario")]
    public class HomeController : Controller
    {
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager)
        {
          _accountServicesForWebApp = accountServicesForWebApp;
          _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userSesion = await _userManager.GetUserAsync(User);

            var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion?.UserName ?? "");
            if (user != null)
            {
                BasicUserViewModel vm = new()
                {
                  Id = user.Id,
                  UserName = user.UserName,
                  Nombre = user.Nombre,
                  Apellido = user.Apellido,
                  Email = user.Email,
                  Role = user.Role,
                  Phone = user.Phone,
                };

                ViewBag.User = vm;
                return View();

            }

            return View();
        }   
    }
}
