using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmsController : Controller
    {
        private readonly IFarmService farmService;
        private readonly IMapper mapper;

        public FarmsController(IFarmService farmService, IMapper mapper)
        {
            this.farmService = farmService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
            => Ok(await farmService.GetAll(
                new Veterinarian() // Temporary solution until this is retrievable from session
                {
                    EmployeeCode = "RS",
                    Regions = new[] { new Region() { Name = "Europe" } }
                }));

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            var farm = await farmService.GetByBlnNumberAsync(blnNumber);
            return farm == null ? NotFound() : Ok(farm);
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmAsync([FromBody] FarmDto farmDto)
        {
            var farm = mapper.Map<Farm>(farmDto);
            await farmService.AddAsync(farm);
            return Ok($"Farm created with BLN number {farmDto.BlnNumber}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFarmByBlnNumberAsync(string blnNumber)
        {
            await farmService.DeleteByBlnNumberAsync(blnNumber);
            return Ok($"Deleted {blnNumber}");
        }
    }
}
