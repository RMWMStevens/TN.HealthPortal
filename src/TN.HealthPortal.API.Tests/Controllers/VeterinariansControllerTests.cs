using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.API.Tests.Controllers
{
    public class VeterinariansControllerTests
    {
        private readonly VeterinariansController sut;
        private readonly Mock<IVeterinarianService> mockVeterinarianService;
        private readonly Mock<IMapper> mockMapper;

        private readonly string vetEmployeeCode = "MC";

        public VeterinariansControllerTests()
        {
            mockVeterinarianService = new Mock<IVeterinarianService>();
            mockMapper = new Mock<IMapper>();

            sut = new VeterinariansController(mockVeterinarianService.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnOkResultWithVeterinarian_WhenVeterinarianExists()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            mockVeterinarianService
                .Setup(_ => _.GetByEmployeeCodeAsync(vetEmployeeCode)).ReturnsAsync(veterinarian);
            var expected = new VeterinarianDto();
            mockMapper
                .Setup(_ => _.Map<VeterinarianDto>(veterinarian))
                .Returns(expected);

            // Act
            var result = await sut.GetByEmployeeCodeAsync(vetEmployeeCode);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnNotFoundResult_WhenVeterinarianNotFound()
        {
            // Arrange
            mockVeterinarianService
                .Setup(_ => _.GetByEmployeeCodeAsync(vetEmployeeCode))
                .ReturnsAsync((Veterinarian)null);

            // Act
            var result = await sut.GetByEmployeeCodeAsync(vetEmployeeCode);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
