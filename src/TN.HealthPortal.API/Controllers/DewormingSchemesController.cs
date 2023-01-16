using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.DropdownOptions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Extensions;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DewormingSchemesController : Controller
    {
        private readonly IDewormingSchemeService dewormingSchemeService;
        private readonly IMapper mapper;

        public DewormingSchemesController(IDewormingSchemeService dewormingSchemeService, IMapper mapper)
        {
            this.dewormingSchemeService = dewormingSchemeService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{blnNumber}")]
        public async Task<IActionResult> GetByBlnNumberAsync(string blnNumber)
        {
            var dewormingSchemes = await dewormingSchemeService.GetByBlnNumberAsync(blnNumber);

            return Ok(mapper.Map<IEnumerable<DewormingSchemeDto>>(dewormingSchemes.Order()));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] DewormingSchemeDto dewormingSchemeDto)
        {
            var dewormingScheme = mapper.Map<DewormingScheme>(dewormingSchemeDto);
            await dewormingSchemeService.AddAsync(dewormingScheme);

            return Ok($"Deworming scheme created for farm with BlnNumber {dewormingSchemeDto.FarmBlnNumber}");
        }

        [HttpGet]
        [Route("GetDropdownOptions")]
        public async Task<IActionResult> GetDropdownOptionsAsync()
        {
            var products = await dewormingSchemeService.GetProductDropdownOptionsAsync();
            var manufacturers = await dewormingSchemeService.GetManufacturerDropdownOptionsAsync();

            return Ok(new DewormingSchemeDropdownOptionsDto()
            {
                Products = mapper.Map<IEnumerable<ProductDto>>(products),
                Manufacturers = mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers),
            });
        }
    }
}
