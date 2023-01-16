using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.API.Helpers;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.Authentication;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using static System.Net.Mime.MediaTypeNames;

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
            var farms = await farmService.GetAllAsync(veterinarian);

            return Ok(mapper.Map<IEnumerable<FarmDto>>(farms));
        }

        [HttpGet]
        [Route("outdated")]
        public async Task<IActionResult> GetAllOutdatedAsync()
        {
            var veterinarian = await identityHelper.GetLoggedInVeterinarianAsync(HttpContext.User.Identity);
            var farms = await farmService.GetAllOutdatedAsync(veterinarian);

            return Ok(mapper.Map<IEnumerable<FarmDto>>(farms));
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            var farm = await farmService.GetByBlnNumberAsync(blnNumber);

            return farm != null
                ? Ok(mapper.Map<FarmDto>(farm))
                : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = AuthenticationRoles.Admin)]
        public async Task<IActionResult> AddFarmAsync([FromBody] FarmDto farmDto)
        {
            var farm = mapper.Map<Farm>(farmDto);
            await farmService.AddAsync(farm);

            return Ok($"Farm created with BLN number {farmDto.BlnNumber}");
        }

        [HttpDelete]
        [Authorize(Roles = AuthenticationRoles.Admin)]
        public async Task<IActionResult> DeleteByBlnNumberAsync(string blnNumber)
        {
            await farmService.DeleteByBlnNumberAsync(blnNumber);

            return Ok($"Deleted {blnNumber}");
        }

        [HttpGet]
        [Route("{blnNumber}/DownloadPdfHealthReport")]
        public async Task<IActionResult> DownloadPdfHealthReportAsync(string blnNumber)
        {
            var fileBytes = await farmService.GeneratePdfHealthReportAsync(blnNumber);
            var fileName = $"{DateTime.UtcNow:yyyy-MM-dd}_HealthReport_{blnNumber}.pdf";

            return Ok(new FileDownloadDto
            {
                FileName = fileName,
                ContentType = Application.Pdf,
                Content = fileBytes,
            });
        }
    }
}
