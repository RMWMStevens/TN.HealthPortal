using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Lib.Models;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : Controller
    {
        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetById(string blnNumber)
        {
            return Ok(new Farm()
            {
                Name = "Farm 1",
                BlnNumber = blnNumber
            });
        }
    }
}
