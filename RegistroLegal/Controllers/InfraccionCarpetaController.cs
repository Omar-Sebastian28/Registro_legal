using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmInfraccionCarpeta;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    [Authorize]
    public class InfraccionCarpetaController : Controller
    {
        private readonly IInfraccionCarpetaServicio _infraccionCarpetaServicio;
        private readonly IIlicitoServicio _ilicitoServicio;
        private readonly IMapper _mapper;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser>  _userManager;
        public readonly ICarpetaServicio _carpetaServicio;

        public InfraccionCarpetaController(IInfraccionCarpetaServicio infraccionCarpetaServicio, IIlicitoServicio ilicitoServicio, IMapper mapper, IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager, ICarpetaServicio carpetaServicio)
        {
            _infraccionCarpetaServicio = infraccionCarpetaServicio;
            _ilicitoServicio = ilicitoServicio;
            _mapper = mapper;
            _userManager = userManager;
            _accountServicesForWebApp = accountServicesForWebApp;
            _carpetaServicio = carpetaServicio;
        }

        public async Task<ActionResult> Index(int carpetaId, string? nombrePersona = null, string? cedula = null)
        {
            if (carpetaId <= 0) 
            {
                return RedirectToRoute(new {controller = "Carpeta", action = "Index"});
            }
            var carpetaDto = await _carpetaServicio.GetById(carpetaId);
            if (carpetaDto != null) 
            {
                var entityDto = await _infraccionCarpetaServicio.GetAllQueryWithInclude(carpetaDto.Id, nombrePersona, cedula);
                var listVm = _mapper.Map<List<BasicInfraccionCarpetaViewModel>>(entityDto);
                return View(listVm);
            }
            return RedirectToRoute(new {controller = "Carpeta", action = "Index"});
        }

        public async Task<IActionResult> AddIlicito(int carpetaId)
        {
            var userSesion = await _userManager.GetUserAsync(User);

            ViewBag.Infracciones = await _ilicitoServicio.GetWithInclude(userSesion?.Id);
            var vm = new CreateInfraccionCarpetaViewModel()
            {
                Id = 0,
                CarpetaId = carpetaId,
                InfraccionId = 0
            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddIlicito(CreateInfraccionCarpetaViewModel vm)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                ViewBag.Infracciones = await _ilicitoServicio.GetWithInclude(userSesion?.Id);
                return View(vm);
            }
            var dto = _mapper.Map<InfraccionCarpetaDto>(vm);

            await _infraccionCarpetaServicio.AddAsync(dto);
            return RedirectToRoute(new {controller = "Carpeta", action = "Index"});
        }


        public async Task<IActionResult> Delete(int id)
        {
            var existDto = await _infraccionCarpetaServicio.GetById(id);

            if (existDto is not null)
            {
                var vm = new DeleteInfraccionCarpetaViewModel()
                {
                    Id = existDto.Id
                };

                return View(vm);
            }
            return RedirectToRoute(new { controller = "Carpeta", action = "Index" });
        }



        [HttpPost]
        public async Task<IActionResult> Delete(DeleteInfraccionCarpetaViewModel vm) 
        {
            if (ModelState.IsValid) 
            {
                var dto = _mapper.Map<InfraccionCarpetaDto>(vm);     
                await _infraccionCarpetaServicio.DeleteAsync(dto.Id);
            }
            return RedirectToRoute(new {controller = "Carpeta", action = "Index"});
        }
    }
}
