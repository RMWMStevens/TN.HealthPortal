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
    public class FarmsControllerTests
    {
        private readonly FarmsController sut;
        private readonly Mock<IFarmService> mockFarmService;
        private readonly Mock<IMapper> mockMapper;

        private readonly string blnNumber = "005630";

        public FarmsControllerTests()
        {
            mockFarmService = new Mock<IFarmService>();
            mockMapper = new Mock<IMapper>();

            sut = new FarmsController(mockFarmService.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithFarms_WhenFarmsLinkedToVeterinarian()
        {
            // Arrange
            var farms = new List<Farm> { new Farm(), new Farm() };
            mockFarmService
                .Setup(_ => _.GetAllAsync(It.IsAny<Veterinarian>()))
                .ReturnsAsync(farms);

            var expected = new List<FarmDto> { new FarmDto(), new FarmDto() };
            mockMapper
                .Setup(_ => _.Map<IEnumerable<FarmDto>>(farms))
                .Returns(expected);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOkResultWithFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm();
            mockFarmService
                .Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(farm);

            var expected = new FarmDto();
            mockMapper
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
            mockFarmService
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
            mockMapper
                .Setup(_ => _.Map<Farm>(farmDto))
                .Returns(farm);

            // Act
            var result = await sut.AddFarmAsync(farmDto);

            // Assert
            mockFarmService.Verify(_ => _.AddAsync(farm), Times.Once());
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
            mockMapper
                .Setup(_ => _.Map<Farm>(farmDto))
                .Returns(farm);

            mockFarmService
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
            mockFarmService.Verify(_ => _.DeleteByBlnNumberAsync(blnNumber), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal($"Deleted {blnNumber}", okResult.Value);
        }
    }
}
