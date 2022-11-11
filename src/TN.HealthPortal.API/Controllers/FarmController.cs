using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.API.Models;
using TN.HealthPortal.Lib.Entities;
using TN.HealthPortal.Lib.Services;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : Controller
    {
        private readonly IFarmService farmService;
        private readonly IMapper mapper;

        public FarmController(IFarmService farmService, IMapper mapper)
        {
            this.farmService = farmService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            try
            {
                return Ok(await farmService.GetByBlnNumberAsync(blnNumber));
            }
            catch
            {
                return StatusCode(404, $"No farm found with this BLN number");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmAsync([FromBody] FarmCreationRequest farmCreationRequest)
        {
            var farm = mapper.Map<Farm>(farmCreationRequest);

            await farmService.AddAsync(farm);
            return Ok($"Farm created with BLN number {farmCreationRequest.BlnNumber}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFarmByBlnNumberAsync(string blnNumber)
        {
            await farmService.DeleteByBlnNumberAsync(blnNumber);
            return Ok($"Deleted {blnNumber}");
        }
    }
}
