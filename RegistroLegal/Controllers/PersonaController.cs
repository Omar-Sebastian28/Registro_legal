using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Aplications.Helpers;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmPersona;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        private readonly IPersonaServicio _personaServicio;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaServicio personaServicio, IMapper mapper, IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager)
        {
            _personaServicio = personaServicio;
            _mapper = mapper;
            _userManager = userManager;
            _accountServicesForWebApp = accountServicesForWebApp;
        }

        public async Task<IActionResult> Index(string? nombrePersona = null, string? cedula = null)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion != null)
            {
                var listDto = await _personaServicio.GetAllWithInclude(userSesion.Id,nombrePersona, cedula);
                var listViewModel = _mapper.Map<List<PersonaViewModelBasic>>(listDto);
                return View(listViewModel);
            }

            return View();
        }

        public async Task<IActionResult> Create()
        {
            var userSesion = await _userManager.GetUserAsync(User); 

            return View(new CreatePersonaViewModel()
            {
                Id = 0,
                NombrePersona = "",
                Apellido = "",
                Cedula = "",
                Telefono = "",
                Nacionalidad = "",
                CreatedById = userSesion?.Id,
                FotoPersona = default
            });
        }

        [HttpPost]
        public  async Task<IActionResult> Create(CreatePersonaViewModel vm) 
        {
            if (!ModelState.IsValid)
            {             
              return View(vm);            
            }

            var dto = _mapper.Map<DtoPersona>(vm);
        
             var returnPerson = await _personaServicio.AddAsync(dto);

            //Control de imagenes para la persona.
            if (returnPerson != null && returnPerson.Id != 0)
            {
                dto.Id = returnPerson.Id;
                dto.FotoPersona = UploadFile.Upload(vm.FotoPersona, dto.Id, "Persona");
                await _personaServicio.UpdateAsync(dto, dto.Id);
            }

            if (returnPerson != null && returnPerson.HasError)
            {
                ModelState.AddModelError("userValidation", returnPerson?.Error ?? "");
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Persona", action = "Index" });      
        }



        public async Task<IActionResult> Edit(int id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion != null)
            {
                var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");
                ViewBag.Roles = user?.Role;
            }

            var dto = await _personaServicio.GetById(id);

            if (dto == null) 
            {
                return RedirectToRoute(new {controller="Persona", action="Index"});       
            }

            CreatePersonaViewModel vm = new()
            {
                NombrePersona = dto.NombrePersona,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono ?? "",
                Cedula = dto.Cedula,
                Nacionalidad = dto.Nacionalidad,
                FotoPersona = default
            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CreatePersonaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = _mapper.Map<DtoPersona>(vm);
            
            //Control de imagenes para la persona.
            string? currentImagePath = "";

            if (currentImagePath is not null)
            {
                currentImagePath = dto.FotoPersona;
            }

            dto.FotoPersona = UploadFile.Upload(vm.FotoPersona, dto.Id, "Persona", true, currentImagePath);
            await _personaServicio.UpdateAsync(dto, dto.Id);

            return RedirectToRoute(new { controller = "Persona", action = "Index" });
        }



        public async Task<IActionResult> Delete(int id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            if (userSesion != null)
            {
                var user = await _accountServicesForWebApp.BuscarUsuarioPorUserName(userSesion.UserName ?? "");
                ViewBag.Roles = user?.Role;
            }

            var dto = await _personaServicio.GetById(id);
            if (dto == null) 
            {
                return RedirectToRoute(new {controller="Persona", action="Index" });
            }

            var vm = _mapper.Map<DeletePersonaViewModel>(dto);
            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(DeletePersonaViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                return View(vm);               
            }
            await _personaServicio.DeleteAsync(vm.Id);
            return RedirectToRoute(new{controller="Persona", action="Index" });
        }
    }
}
