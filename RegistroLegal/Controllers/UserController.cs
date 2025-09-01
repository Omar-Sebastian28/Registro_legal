using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.UserDto;
using RegistroLegal.Core.Aplications.Helpers;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmUser;
using RegistroLegal.Core.Domain.Enums.RolesUser;
using RegistroLegal.Infraestructura.Identity.Entities;


namespace RegistroLegal.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IEmailServices _emailServices;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        public readonly UserManager<AppUser> _userManager;

        public UserController(IEmailServices emailServices, IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager)
        {
            _emailServices = emailServices;
            _accountServicesForWebApp = accountServicesForWebApp;
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
             var listDto =  await _accountServicesForWebApp.GetAllUser(true);
             var listViewModel = listDto.Select(u => new BasicUserViewModel()
             {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                UserName = u.UserName,
                Email = u.Email,
                Phone = u.Phone,
                ImagenPerfil = u.ImagenPerfil,
                Role = u.Role,  
             }).ToList();
                 return View(listViewModel);
        }


        public async Task<IActionResult> Create()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion is null)
            {
                ViewBag.Registrado = false;
            }

            return View(new SaveUserViewModel()
            {
                Id = "",
                Nombre = "",
                Apellido = "",
                UserName = "",
                ConfirmPasword = "",
                Email = "",
                Password = "",
                Phone = "",
                Role = "",
            });   
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Registrado = false;
                return View(vm);
            }

            var origin = Request.Headers.Origin.ToString();
            SaveUserDto dto = new()
            {
                Id = "",
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                Phone = vm.Phone,
                Role = Roles.Usuario.ToString(),
            };

            var returnUser = await _accountServicesForWebApp.RegisterAsync(dto, origin);
            if (!string.IsNullOrEmpty(returnUser.Id))
            {
                dto.Id = returnUser.Id;
                dto.ImagenPerfil = UploadFile.Upload(vm.ImagenPerfilFile, dto.Id, "Users");
                await _accountServicesForWebApp.EditUser(dto, origin, true);
            }

            if (returnUser != null && returnUser.HasError == true) 
            {
                ModelState.AddModelError("userValidation", returnUser?.Error ?? "");
                return View(vm);      
            }        
            
            return RedirectToRoute(new {controller = "User", action = "Index"});       
        }

        [Authorize(Roles = "Admin, Usuario")]
        public async Task<IActionResult> Edit(string Id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var userDto = await _accountServicesForWebApp.BuscarUsuarioPorId(Id ?? userSesion?.Id ?? "");

            if (userDto == null)
            {
                return RedirectToRoute(new {controller = "User", action = "Index"});
            }

            UpdateUserViewModel vm = new()
            {
                Id = userDto.Id,
                Nombre = userDto.Nombre,
                Apellido = userDto.Apellido,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = "",
                Phone = userDto.Phone,
                ImagenPerfilFile = default,
                Role = Roles.Usuario.ToString(),
            };
            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var userSesion = await _userManager.GetUserAsync(User);
            var user = await _accountServicesForWebApp.BuscarUsuarioPorId(userSesion?.Id ?? "");

            SaveUserDto dto = new()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password ?? "",
                Phone = vm.Phone, 
                Role = user?.Role ?? ""
            };

            var origin = Request.Headers.Origin.ToString();
           
            string? currentImagePath = dto.ImagenPerfil;

            dto.ImagenPerfil = UploadFile.Upload(vm.ImagenPerfilFile, dto.Id, "User", true, currentImagePath);

            var responseUser = await _accountServicesForWebApp.EditUser(dto, origin);

            if (responseUser == null || responseUser.HasError) 
            {
                ModelState.AddModelError("userValidation", responseUser?.Error ?? "");
                return View(vm);
            }
            
            var destino = user?.Role == Roles.Usuario.ToString() ? "Home" : "User";
            return RedirectToRoute(new {controller = destino, action = "Index" });
        }


        [Authorize(Roles = "Admin, Usuario")]
        public async Task<IActionResult> Delete(string Id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var user = await _accountServicesForWebApp.BuscarUsuarioPorId(Id ?? userSesion?.Id ?? "");

            var dto = await _accountServicesForWebApp.BuscarUsuarioPorId(user?.Id ?? "");
            if (dto == null) 
            {
                ModelState.AddModelError("userValidation", "No se pudo encontrar el usuario" ?? "");
                return RedirectToRoute(new {controller="User", action="Index" });
            }

            DeleteUserViewModel vm = new()
            {
                Id = dto.Id,
                UserName = dto.Nombre,
                LastName = dto.Apellido,
            };

            if (userSesion != null)
            {
                var userRol = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");
                ViewBag.Roles = userRol?.Role;
                return View(vm);
            }
            return View(vm);
        }

 
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel vm)
        {
            var userSeion = await _userManager.GetUserAsync(User);
            var user = await _accountServicesForWebApp.BuscarUsuarioPorId(userSeion?.Id ?? "");

            if (!ModelState.IsValid) 
            {
                return View(vm);    
            }
            
            var DeleteresponseDto = await _accountServicesForWebApp.DeleteAsync(vm.Id);
            if (!DeleteresponseDto.HasError)
            {
                UploadFile.Delete(vm.Id, "Users");
            }
            else 
            {
                ModelState.AddModelError("userValidation", DeleteresponseDto.Error ?? "");
                return View(vm);
            }

            bool isAdmin = Roles.Admin.ToString().Equals(user?.Role, StringComparison.OrdinalIgnoreCase);
            bool adminEliminandoProppiaCuenta = vm.Id.Equals(user?.Id);

            var destino = isAdmin && !adminEliminandoProppiaCuenta ? "User" : "Login";
            return RedirectToRoute(new {controller = destino, action = "Index" });
        }
    }
}
