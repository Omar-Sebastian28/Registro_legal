using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.IlicitoDto;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmIlicito;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    [Authorize]
    public class InfraccionController : Controller
    {
        private readonly IIlicitoServicio _ilicitoServicio;
        private readonly IPersonaServicio _personaServicio;
        private readonly IMedioServicio _medioServicio;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public InfraccionController(IIlicitoServicio ilicito, IPersonaServicio personaServicio, IMedioServicio medioServicio, IMapper mapper, IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager )
        {
            _ilicitoServicio = ilicito;
            _personaServicio = personaServicio;
            _medioServicio = medioServicio;
            _mapper = mapper;
            _accountServicesForWebApp = accountServicesForWebApp;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var entities = await _ilicitoServicio.GetWithInclude(userSesion?.Id);

            if (entities is null)
            {
                return View();
            }

            var listViewModel = _mapper.Map<List<BasicIlicitoViewModel>>(entities);

            return View(listViewModel);
        }


        public async Task<IActionResult> Create()
        {
            var userSesion = await _userManager.GetUserAsync(User);

            ViewBag.persona = await _personaServicio.GetAllListAsync();
            ViewBag.medio = await _medioServicio.GetAllListAsync();

            return View(new CreateIlicitoViewModel()
            {
                Tipo = "",
                Descripcion = "",
                PersonaId = 0,
                MedioId = 0,
                CreatedById = userSesion?.Id
            });
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateIlicitoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.persona = await _personaServicio.GetAllListAsync();
                ViewBag.medio = await _medioServicio.GetAllListAsync();
                return View(vm);
            }
         
            var dto = _mapper.Map<DtoBasicIlicito>(vm);

            if (dto is not null)
            {
                await _ilicitoServicio.AddAsync(dto);
            }            
            return RedirectToRoute(new { controller = "Infraccion", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id) 
        {
            var userSesion = await _userManager.GetUserAsync(User);

            ViewBag.persona = await _personaServicio.GetAllListAsync();
            ViewBag.medio = await _medioServicio.GetAllListAsync();

            var dto = await _ilicitoServicio.GetById(id);

            if (dto is null) 
            {         
                return RedirectToRoute(new {controller="Infraccion", action="Index"});                        
            }

            var updateIlicitoVm = _mapper.Map<UpdateIlicitoViewModel>(dto);

            return View(updateIlicitoVm);        
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateIlicitoViewModel vm)
        {

            if (!ModelState.IsValid) 
            {
                ViewBag.persona = await _personaServicio.GetAllListAsync();
                ViewBag.medio = await _medioServicio.GetAllListAsync();
                return View(vm);    
            }

            var updateIlicitoDto = _mapper.Map<DtoBasicIlicito>(vm);

            await _ilicitoServicio.UpdateAsync(updateIlicitoDto, updateIlicitoDto.Id);
            return RedirectToRoute(new {controller = "Infraccion", action = "Index"});
        }


        public async Task<IActionResult> Delete(int id)
        {
           var dto = await _ilicitoServicio.GetById(id);
            if (dto is null)
            {
                return RedirectToRoute(new { controller = "Infraccion", action = "Index" });
            }

            return View(_mapper.Map<DeleteInfraccionViewModel>(dto));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(DeleteInfraccionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _ilicitoServicio.DeleteAsync(vm.Id);
            return RedirectToRoute(new {controller = "Infraccion", action = "Index"});
        }
    }
}
