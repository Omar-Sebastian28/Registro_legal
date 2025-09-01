using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.UserDto;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmUser;
using RegistroLegal.Core.Domain.Enums.RolesUser;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager)
        {
            _accountServicesForWebApp = accountServicesForWebApp;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSesion = await _userManager.GetUserAsync(User);
            if (userSesion != null)
            {
                var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");

                if (user != null && user.Role == Roles.Admin.ToString())
                {
                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }
                else if (user != null && user.Role == Roles.Usuario.ToString())
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }

            var vm = new LoginViewModel()
            {
                UserName = "",
                Password = ""
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // vm.Password = "";
                return View(vm);
            }

            LoginReponseDto loginReponseDto = await _accountServicesForWebApp.AuthenticateAsync(new LoginDto()
            {
                UserName = vm.UserName,
                Password = vm.Password
            });

            if (loginReponseDto is not null)
            {
                if (loginReponseDto.Roles != null && loginReponseDto.Roles.Any(r => r == Roles.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }
                else if (loginReponseDto.Roles != null && loginReponseDto.Roles.Any(r => r == Roles.Usuario.ToString()))
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }

            ModelState.AddModelError("userValidation", loginReponseDto?.Error ?? "");
            return View(vm);
        }


        public async Task<IActionResult> LogOut()
        {
            await _accountServicesForWebApp.SingOutAsync();
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        public IActionResult ForgotPassword()
        {
            return View(new ResetPasswordResponseVieModel() { UserName = "", Origin = "" });
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetPasswordResponseVieModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers.Origin.ToString();

            ResetPasswordResponseDto dto = new()
            {
                UserName = vm.UserName,
                Origin = origin
            };

            var reponse = await _accountServicesForWebApp.ForgotPassword(dto);
            if (reponse.HasError)
            {
                ModelState.AddModelError("userValidation", reponse.Error ?? "");
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        public async Task<IActionResult> AccesoDenegado()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion is not null)
            {
                var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");
                if (user?.Role is not null)
                {
                    ViewBag.rolActual = user.Role;
                    return View();
                }
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string reponse = await _accountServicesForWebApp.ConfirmAccount(userId, token);
            return View("ConfirmEmail", reponse);
        }



        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordRequestViewModel() { Id = userId, Token = token, Password = "" });
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                return View(vm);
            }

            ResetPasswordRequestDto dto = new()
            {
                Id = vm.Id,
                Token = vm.Token,
                Password = vm.Password
            };

            var response = await _accountServicesForWebApp.ConfirmForgotPassword(dto);
            if (response.HasError) 
            {
                ModelState.AddModelError("userValidation", response.Error ?? "");
                return View(vm);
            }

            return RedirectToRoute(new {controller = "Login", action = "Index"});
        } 
    }
}
