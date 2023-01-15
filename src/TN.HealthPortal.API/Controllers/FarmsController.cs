using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.Authentication;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmsController : Controller
    {
        private readonly IFarmService farmService;
        private readonly IVeterinarianService veterinarianService;
        private readonly IMapper mapper;

        public FarmsController(
            IFarmService farmService,
            IVeterinarianService veterinarianService,
            IMapper mapper)
        {
            this.farmService = farmService;
            this.veterinarianService = veterinarianService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest("Failed to retrieve the logged in veterinarian's identity");

            var veterinarian = await veterinarianService.GetByEmployeeCodeAsync(
                identity.FindFirst(ClaimTypes.Name)?.Value);
            if (veterinarian == null)
                return BadRequest("Failed to retrieve the logged in veterinarian's EmployeeCode");

            var farms = await farmService.GetAllAsync(veterinarian);
            return Ok(mapper.Map<IEnumerable<FarmDto>>(farms));
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            var farm = await farmService.GetByBlnNumberAsync(blnNumber);
            return farm == null ? NotFound() : Ok(mapper.Map<FarmDto>(farm));
        }

        [HttpPost]
        [Authorize(Roles = AuthenticationRoles.Admin)]
        public async Task<IActionResult> AddFarmAsync([FromBody] FarmDto farmDto)
        {
            try
            {
                var farm = mapper.Map<Farm>(farmDto);
                await farmService.AddAsync(farm);
                return Ok($"Farm created with BLN number {farmDto.BlnNumber}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = AuthenticationRoles.Admin)]
        public async Task<IActionResult> DeleteFarmByBlnNumberAsync(string blnNumber)
        {
            await farmService.DeleteByBlnNumberAsync(blnNumber);
            return Ok($"Deleted {blnNumber}"); // TODO: What if the farm with given BlnNumber does not exist?
        }
    }
}
