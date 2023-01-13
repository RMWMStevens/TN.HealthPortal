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
        public async Task<IActionResult> GetAllAsync()
        {
            var farms = await farmService.GetAllAsync(
                new Veterinarian() // TODO: Temporary solution until this is retrievable from session
                {
                    EmployeeCode = "MC",
                    Regions = new[] { new Region() { Name = "Europe" }, new Region() { Name = "Americas" } }
                });
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
        public async Task<IActionResult> DeleteFarmByBlnNumberAsync(string blnNumber)
        {
            await farmService.DeleteByBlnNumberAsync(blnNumber);
            return Ok($"Deleted {blnNumber}"); // TODO: What if the farm with given BlnNumber does not exist?
        }
    }
}
