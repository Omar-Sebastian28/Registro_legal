using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.Helpers;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.ViewModel.VmMedio;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Controllers
{
    [Authorize]
    public class MedioController : Controller
    {
        private readonly IMedioServicio _medioServicio;
        private readonly IMapper _mapper;
        private readonly IAccountServicesForWebApp _accountServicesForWebApp;
        private readonly UserManager<AppUser> _userManager;

        public MedioController(IMedioServicio medioServicio, IMapper mapper, IAccountServicesForWebApp accountServicesForWebApp, UserManager<AppUser> userManager)
        {
            _medioServicio = medioServicio;
            _mapper = mapper;
            _userManager = userManager;
            _accountServicesForWebApp = accountServicesForWebApp;
        }

        public async Task<IActionResult> Index()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var listEntity = await _medioServicio.GetAllWithInclude(userSesion?.Id);

            var listVm = _mapper.Map<List<BasicMedioViewModel>>(listEntity).ToList();

            if (listVm == null) 
            {
                return View(new List<BasicMedioViewModel>());
            }
            return View(listVm);
        }


        public async Task<IActionResult> Create()
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var entity = new CreateMedioViewModel()
            {
                Id = 0,
                NombreMedio = "",
                Descripcion = "",
                Foto = default,
                LinkReferencia = "",
                CreatedById = userSesion?.Id
            };
            return View(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateMedioViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = _mapper.Map<MedioDto>(vm); 
            var returnMedio = await _medioServicio.AddAsync(dto);

            //Control de imagenes para la persona.
            if (returnMedio != null && returnMedio.Id != 0)
            {
                dto.Id = returnMedio.Id;
                dto.Foto = UploadFile.Upload(vm.Foto, dto.Id, "Persona");
                await _medioServicio.UpdateAsync(dto, dto.Id);

            }
            return RedirectToRoute(new {controller = "Medio", action = "Index"});
        }



        public async Task<IActionResult> Edit(int id)
        {
            var entitydto = await _medioServicio.GetById(id);

            if (entitydto != null)
            {
                var vm = new CreateMedioViewModel()
                {
                    Id = entitydto.Id,
                    NombreMedio = entitydto.NombreMedio,
                    Descripcion = entitydto.Descripcion,
                    Foto = default,  
                    LinkReferencia = entitydto.LinkReferencia
                };

                return View(vm);
            }
            return RedirectToRoute(new {controller = "Medio", action = "Index"});
        }



        [HttpPost]
        public async Task<IActionResult> Edit(CreateMedioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entity = await _medioServicio.GetById(vm.Id);

            if (entity != null)
            {
                var dto = _mapper.Map<MedioDto>(vm);

                //Control de imagenes para la persona.
                string? currentImagePath = "";

                if (currentImagePath is not null)
                {
                    currentImagePath = dto.Foto;
                }

                dto.Foto = UploadFile.Upload(vm.Foto, dto.Id, "Persona", true, currentImagePath);
                await _medioServicio.UpdateAsync(dto, dto.Id);
            }
            return RedirectToRoute(new { controller = "Medio", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var userSesion = await _userManager.GetUserAsync(User);
            var entityDto = await _medioServicio.GetById(id);

            if (entityDto != null)
            {
                var vm = _mapper.Map<DeleteMedioViewModel>(entityDto);
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Medio", action = "Index" });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMedioViewModel vm) 
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _medioServicio.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Medio", action = "Index" });
        }   
    }
}
