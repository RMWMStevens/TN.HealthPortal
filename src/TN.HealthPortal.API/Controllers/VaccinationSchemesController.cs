using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
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
            try
            {
                var vaccinationSchemes = await vaccinationSchemeService.GetByBlnNumberAsync(blnNumber);
                return Ok(mapper.Map<IEnumerable<VaccinationSchemeDto>>(vaccinationSchemes));
            }
            catch
            {
                return NotFound($"No Vaccination schemes found with BLN number {blnNumber}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccinationSchemeAsync([FromBody] VaccinationSchemeDto vaccinationSchemeDto)
        {
            var vaccinationScheme = mapper.Map<VaccinationScheme>(vaccinationSchemeDto);
            await vaccinationSchemeService.AddAsync(vaccinationScheme);
            return Ok($"Vaccination scheme created with BLN number {vaccinationSchemeDto.Product.Name}");
        }
    }
}
