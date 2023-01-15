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
        private readonly Mock<IVeterinarianService> veterinarianServiceMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly string veterinarianEmployeeCode = "EmployeeCode";

        public VeterinariansControllerTests()
        {
            veterinarianServiceMock = new Mock<IVeterinarianService>();
            mapperMock = new Mock<IMapper>();

            sut = new VeterinariansController(veterinarianServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnOkResultWithVeterinarian_WhenVeterinarianExists()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            veterinarianServiceMock
                .Setup(_ => _.GetByEmployeeCodeAsync(veterinarianEmployeeCode)).ReturnsAsync(veterinarian);
            var expected = new VeterinarianDto();
            mapperMock
                .Setup(_ => _.Map<VeterinarianDto>(veterinarian))
                .Returns(expected);

            // Act
            var result = await sut.GetByEmployeeCodeAsync(veterinarianEmployeeCode);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnNotFoundResult_WhenVeterinarianNotFound()
        {
            // Arrange
            veterinarianServiceMock
                .Setup(_ => _.GetByEmployeeCodeAsync(veterinarianEmployeeCode))
                .ReturnsAsync((Veterinarian)null);

            // Act
            var result = await sut.GetByEmployeeCodeAsync(veterinarianEmployeeCode);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
