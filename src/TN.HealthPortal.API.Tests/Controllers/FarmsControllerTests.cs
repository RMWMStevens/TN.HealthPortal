using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.API.Tests.Controllers
{
    public class FarmsControllerTests
    {
        private readonly FarmsController sut;
        private readonly Mock<IFarmService> farmServiceMock;
        private readonly Mock<IVeterinarianService> veterinarianServiceMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly string blnNumber = "005630";

        public FarmsControllerTests()
        {
            farmServiceMock = new Mock<IFarmService>();
            veterinarianServiceMock = new Mock<IVeterinarianService>();
            mapperMock = new Mock<IMapper>();

            sut = new FarmsController(farmServiceMock.Object, veterinarianServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnBadRequest_WhenIdentityIsNull()
        {
            // Arrange
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.User = new ClaimsPrincipal();

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve the logged in veterinarian's identity", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnBadRequest_WhenVeterinarianNotFound()
        {
            // Arrange
            var employeeCode = "EmployeeCode";
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, employeeCode));
            sut.ControllerContext.HttpContext.User = new ClaimsPrincipal(identity);
            veterinarianServiceMock.Setup(_ => _.GetByEmployeeCodeAsync(employeeCode)).ReturnsAsync((Veterinarian)null);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve the logged in veterinarian's EmployeeCode", badRequestResult.Value);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOkResultWithFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm();
            farmServiceMock
                .Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(farm);

            var expected = new FarmDto();
            mapperMock
                .Setup(_ => _.Map<FarmDto>(farm))
                .Returns(expected);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnNotFoundResult_WhenFarmNotFound()
        {
            // Arrange
            farmServiceMock
                .Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync((Farm)null);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddFarmAsync_ShouldReturnOkResultWithFarmBlnNumber_WhenFarmCreated()
        {
            // Arrange
            var farmDto = new FarmDto { BlnNumber = blnNumber };
            var farm = new Farm();
            mapperMock
                .Setup(_ => _.Map<Farm>(farmDto))
                .Returns(farm);

            // Act
            var result = await sut.AddFarmAsync(farmDto);

            // Assert
            farmServiceMock.Verify(_ => _.AddAsync(farm), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal($"Farm created with BLN number {blnNumber}", okResult.Value);
        }

        [Fact]
        public async Task AddFarmAsync_ShouldReturnBadRequestResult_WhenArgumentExceptionThrown()
        {
            // Arrange
            var farmDto = new FarmDto();
            var farm = new Farm();
            mapperMock
                .Setup(_ => _.Map<Farm>(farmDto))
                .Returns(farm);

            farmServiceMock
                .Setup(_ => _.AddAsync(farm))
                .ThrowsAsync(new ArgumentException("Invalid farm data"));

            // Act
            var result = await sut.AddFarmAsync(farmDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Invalid farm data", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteFarmByBlnNumberAsync_ShouldReturnOkResultWithDeletedBlnNumber_WhenFarmDeleted()
        {
            // Act
            var result = await sut.DeleteFarmByBlnNumberAsync(blnNumber);

            // Assert
            farmServiceMock.Verify(_ => _.DeleteByBlnNumberAsync(blnNumber), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal($"Deleted {blnNumber}", okResult.Value);
        }
    }
}
