using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmCarpeta;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    [Authorize]
    public class CarpetaController : Controller
    {
        private readonly ICarpetaServicio _carpetaServicio;
        private readonly IMapper _mapper;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;

        public CarpetaController(ICarpetaServicio carpetaServicio, IMapper mapper, UserManager<AppUser> userManager, IAccountServicesForWebApp accountServicesForWebApp)
        {
            _carpetaServicio = carpetaServicio;
            _mapper = mapper;
            _userManager = userManager;
            _accountServicesForWebApp = accountServicesForWebApp;
        }

        public async Task<ActionResult> Index()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var listDto = await _carpetaServicio.GetWithInclude(userSesion?.Id);

            if (!listDto.IsNullOrEmpty())
            {
                var listVm = _mapper.Map<List<BasicCarpetaViewModel>>(listDto).ToList();
                return View(listVm);
            }

            return View(new List<BasicCarpetaViewModel>());
        }

        public async Task<IActionResult> Create()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var entity = new CreateCarpetaViewModel()
            {
                Id = 0,
                Nombre = "",
                CreatedById = userSesion?.Id
            };
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarpetaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entityDto = _mapper.Map<CarpetasDto>(vm);
            await _carpetaServicio.AddAsync(entityDto);

            return RedirectToRoute(new { controller = "Carpeta", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion != null)
            {
                var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");
                ViewBag.Roles = user?.Role;
            }

            var entityDto = await _carpetaServicio.GetById(id);

            if (entityDto is not null)
            {
                var vm = _mapper.Map<CreateCarpetaViewModel>(entityDto);
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Carpeta", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateCarpetaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entityDto = _mapper.Map<CarpetasDto>(vm);

            await _carpetaServicio.UpdateAsync(entityDto, entityDto.Id);

            return RedirectToRoute(new { controller = "Carpeta", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var entityDto = await _carpetaServicio.GetById(id);

            if (entityDto != null)
            {
                var vm = _mapper.Map<DeleteCarpetaViewModel>(entityDto);
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Carpeta", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCarpetaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _carpetaServicio.DeleteAsync(vm.Id);
            return RedirectToRoute(new {controller = "Carpeta", action = "Index"});
        }
    }
}
