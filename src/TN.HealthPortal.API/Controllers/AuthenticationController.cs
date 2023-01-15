using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TN.HealthPortal.Logic.DTOs.Authentication;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IVeterinarianService veterinarianService;
        private readonly IConfiguration configuration;

        public AuthenticationController(
            IVeterinarianService veterinarianService,
            IConfiguration configuration)
        {
            this.veterinarianService = veterinarianService;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
        {
            var veterinarian = await veterinarianService.GetByEmployeeCodeAsync(login.EmployeeCode);
            if (veterinarian == null)
                return Unauthorized();

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, veterinarian.EmployeeCode),
                new Claim(ClaimTypes.Role, AuthenticationRoles.Veterinarian)
            };
            if (veterinarian.IsAdmin)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, AuthenticationRoles.Admin));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddSeconds(int.Parse(configuration["JWT:ExpiresIn"])),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo
            });
        }
    }
}
