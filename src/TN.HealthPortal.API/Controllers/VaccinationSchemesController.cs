using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.DropdownOptions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Extensions;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationSchemesController : Controller
    {
        private readonly IVaccinationSchemeService vaccinationSchemeService;
        private readonly IMapper mapper;

        public VaccinationSchemesController(IVaccinationSchemeService vaccinationSchemeService, IMapper mapper)
        {
            this.vaccinationSchemeService = vaccinationSchemeService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            var vaccinationSchemes = await vaccinationSchemeService.GetByBlnNumberAsync(blnNumber);

            return Ok(mapper.Map<IEnumerable<VaccinationSchemeDto>>(vaccinationSchemes.Order()));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] VaccinationSchemeDto vaccinationSchemeDto)
        {
            var vaccinationScheme = mapper.Map<VaccinationScheme>(vaccinationSchemeDto);
            await vaccinationSchemeService.AddAsync(vaccinationScheme);

            return Ok($"Vaccination scheme created for farm with BlnNumber {vaccinationSchemeDto.FarmBlnNumber}");
        }

        [HttpGet]
        [Route("GetDropdownOptions")]
        public async Task<IActionResult> GetDropdownOptionsAsync()
        {
            var pathogens = await vaccinationSchemeService.GetPathogenDropdownOptionsAsync();
            var products = await vaccinationSchemeService.GetProductDropdownOptionsAsync();
            var manufacturers = await vaccinationSchemeService.GetManufacturerDropdownOptionsAsync();

            return Ok(new VaccinationSchemeDropdownOptionsDto()
            {
                Pathogens = mapper.Map<IEnumerable<PathogenDto>>(pathogens),
                Products = mapper.Map<IEnumerable<ProductDto>>(products),
                Manufacturers = mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers),
            });
        }
    }
}
