using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.Logic.DTOs.Authentication;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.API.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly AuthenticationController sut;
        private readonly Mock<IVeterinarianService> veterinarianServiceMock;
        private readonly Mock<IConfiguration> configurationMock;

        public AuthenticationControllerTests()
        {
            veterinarianServiceMock = new Mock<IVeterinarianService>();
            configurationMock = new Mock<IConfiguration>();

            sut = new AuthenticationController(
                veterinarianServiceMock.Object,
                configurationMock.Object
            );
        }

        [Fact]
        public async void LoginAsync_ShouldReturnUnauthorized_WhenCredentialsInvalid()
        {
            // Arrange
            var login = new LoginDto { EmployeeCode = "invalid" };
            veterinarianServiceMock
                .Setup(_ => _.GetByEmployeeCodeAsync(login.EmployeeCode))
                .ReturnsAsync((Veterinarian)null);

            // Act
            var result = await sut.LoginAsync(login);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async void LoginAsync_ShouldReturnOk_WhenCredentialsValid()
        {
            // Arrange
            var login = new LoginDto { EmployeeCode = "valid" };
            var veterinarian = new Veterinarian { EmployeeCode = "valid", IsAdmin = true };
            veterinarianServiceMock
                .Setup(_ => _.GetByEmployeeCodeAsync(login.EmployeeCode))
                .ReturnsAsync(veterinarian);
            configurationMock
                .Setup(configuration => configuration["JWT:Secret"])
                .Returns("ThisIsAVeryStrongSecretForTestingPurposes");
            configurationMock
                .Setup(configuration => configuration["JWT:ValidIssuer"])
                .Returns("issuer");
            configurationMock
                .Setup(configuration => configuration["JWT:ValidAudience"])
                .Returns("audience");
            configurationMock
                .Setup(configuration => configuration["JWT:ExpiresIn"])
                .Returns("600");

            // Act
            var result = await sut.LoginAsync(login);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var tokenDto = (result as OkObjectResult).Value as TokenDto;
            Assert.NotNull(tokenDto.Token);
            Assert.True(tokenDto.ExpiresAt > DateTime.UtcNow);
        }
    }
}
