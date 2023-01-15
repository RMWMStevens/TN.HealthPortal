using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.API.Helpers;
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
        private readonly IIdentityHelper identityHelper;
        private readonly IMapper mapper;

        public FarmsController(
            IFarmService farmService,
            IIdentityHelper identityHelper,
            IMapper mapper)
        {
            this.farmService = farmService;
            this.identityHelper = identityHelper;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var veterinarian = await identityHelper.GetLoggedInVeterinarianAsync(HttpContext.User.Identity);
            if (veterinarian == null)
                return BadRequest("Failed to retrieve the logged in veterinarian's identity");

            var farms = await farmService.GetAllAsync(veterinarian);
            return Ok(mapper.Map<IEnumerable<FarmDto>>(farms));
        }

        [HttpGet]
        [Route("outdated")]
        public async Task<IActionResult> GetAllOutdatedAsync()
        {
            var veterinarian = await identityHelper.GetLoggedInVeterinarianAsync(HttpContext.User.Identity);
            if (veterinarian == null)
                return BadRequest("Failed to retrieve the logged in veterinarian's identity");

            var farms = await farmService.GetAllOutdatedAsync(veterinarian);
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
