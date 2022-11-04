using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Lib.Models;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : Controller
    {
        private readonly ITempFarmRepository tempFarmRepository;

        public FarmController(ITempFarmRepository tempFarmRepository)
        {
            this.tempFarmRepository = tempFarmRepository;
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumber(string blnNumber)
        {
            try
            {
                return Ok(tempFarmRepository.GetByBlnNumber(blnNumber));
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
                var existingFarm = tempFarmRepository.GetByBlnNumber(blnNumber);
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
                    Source = new Source
                    {
                        Gilt = "Closed Herd",
                        Boar = "Closed Herd",
                        Semen = "S35 GTC",
                        History = "Too long, didn't type :)"
                    },
                    BlnNumber = blnNumber,
                    Description = "This is a farm from the first PIC Farm Profile Report",
                };

                tempFarmRepository.Add(farm);

                return Ok($"Farm created with BLN number {blnNumber}");
            }
        }
    }
}
