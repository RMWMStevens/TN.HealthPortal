using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Lib.Models;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinarianController : Controller
    {
        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<IActionResult> GetById(string employeeCode)
        {
            return Ok(new Veterinarian()
            {
                Name = "John Doe",
                EmployeeCode = employeeCode,
                Farms = new List<Farm>()
                {
                    new Farm
                    {
                        Name = "Van Beek",
                        BlnNumber = "106860",
                        PremiseID = "15",
                        Description = "GN farm producing Z-line",
                        Address = "Janssen straat 58, 8546WD Gelderland, Nederland",
                        SiteType = "Genetic Nucleus",
                        Capacity = 3500
                    },
                    new Farm
                    {
                        Name = "Ashorst",
                        BlnNumber = "005630",
                        PremiseID = "16",
                        Description = "Ingene farm in Germany",
                        Address = "Die strasse 5, 78546 Noordrijn-Westfalen, Deutschland",
                        SiteType = "Ingene",
                        Capacity = 7500
                    }
                }
            });
        }
    }
}
