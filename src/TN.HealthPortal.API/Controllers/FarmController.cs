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
        public async Task<IActionResult> GetByBlnNumber(string blnNumber)
        {
            try
            {
                return Ok(farmService.GetByBlnNumber(blnNumber));
            }
            catch
            {
                return StatusCode(404, $"No farm found with this BLN number");
                throw;
            }
        }

        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> AddIfNotExists()
        {
            var blnNumber = "123456";

            try
            {
                var existingFarm = farmService.GetByBlnNumber(blnNumber);
                return Ok($"Farm with BLN number {blnNumber} already exists");
            }
            catch
            {
                var farm = new Farm
                {
                    Name = "MOLLEVANG - MV1",
                    PremiseID = "90432",
                    Address = "Holsted, Jutland, Denmark",
                    SiteType = "Farrow to finish",
                    Capacity = 1250,
                    History = "Too long, didn't type :)",
                    Source = new Source
                    {
                        Gilt = "Closed Herd",
                        Boar = "Closed Herd",
                        Semen = "S35 GTC",
                    },
                    BlnNumber = blnNumber,
                    Description = "This is a farm from the first PIC Farm Profile Report",
                };

                farmService.Add(farm);

                return Ok($"Farm created with BLN number {blnNumber}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFarm([FromBody] FarmCreationRequest farmCreationRequest)
        {
            var farm = mapper.Map<Farm>(farmCreationRequest);

            farmService.AddFarmAsync(farm);
            return Ok($"Farm created with BLN number {farmCreationRequest.BlnNumber}");
        }
    }
}
