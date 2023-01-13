using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinariansController : Controller
    {
        private readonly IVeterinarianService veterinarianService;
        private readonly IMapper mapper;

        public VeterinariansController(IVeterinarianService veterinarianService, IMapper mapper)
        {
            this.veterinarianService = veterinarianService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<IActionResult> GetByEmployeeCodeAsync(string employeeCode)
        {
            var veterinarian = await veterinarianService.GetByEmployeeCodeAsync(employeeCode);
            return veterinarian == null ? NotFound() : Ok(mapper.Map<VeterinarianDto>(veterinarian));
        }
    }
}
