using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.API.Helpers;
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
        private readonly Mock<IIdentityHelper> identityHelperMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly string blnNumber = "005630";

        public FarmsControllerTests()
        {
            farmServiceMock = new Mock<IFarmService>();
            identityHelperMock = new Mock<IIdentityHelper>();
            mapperMock = new Mock<IMapper>();

            sut = new FarmsController(farmServiceMock.Object, identityHelperMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOk()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            var farms = new List<Farm> { new Farm() };
            var expected = new List<FarmDto> { new FarmDto() };

            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.User = new ClaimsPrincipal();

            identityHelperMock.Setup(_ => _.GetLoggedInVeterinarianAsync(It.IsAny<ClaimsIdentity>()))
                .ReturnsAsync(veterinarian);
            farmServiceMock.Setup(_ => _.GetAllAsync(veterinarian))
                .ReturnsAsync(farms);
            mapperMock.Setup(_ => _.Map<IEnumerable<FarmDto>>(farms))
                .Returns(expected);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<FarmDto>>(okResult.Value);
            Assert.Equal(expected, returnValue);
        }

        [Fact]
        public async Task GetAllOutdatedAsync_ShouldReturnOk()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            var farms = new List<Farm> { new Farm() };
            var farmsDto = new List<FarmDto> { new FarmDto() };

            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.User = new ClaimsPrincipal();

            identityHelperMock.Setup(x => x.GetLoggedInVeterinarianAsync(It.IsAny<ClaimsIdentity>()))
                .ReturnsAsync(veterinarian);
            farmServiceMock.Setup(_ => _.GetAllOutdatedAsync(veterinarian))
                .ReturnsAsync(farms);
            mapperMock.Setup(_ => _.Map<IEnumerable<FarmDto>>(farms))
                .Returns(farmsDto);

            // Act
            var result = await sut.GetAllOutdatedAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<FarmDto>>(okResult.Value);
            Assert.Equal(farmsDto, returnValue);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOk_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm();
            var farmDto = new FarmDto();

            farmServiceMock.Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(farm);
            mapperMock.Setup(_ => _.Map<FarmDto>(farm))
                .Returns(farmDto);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FarmDto>(okResult.Value);
            Assert.Equal(farmDto, returnValue);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnNotFound_WhenFarmNotExists()
        {
            // Arrange
            farmServiceMock.Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync((Farm)null);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddFarmAsync_ShouldReturnOk()
        {
            // Arrange
            var farmDto = new FarmDto { BlnNumber = blnNumber };
            var farm = new Farm { BlnNumber = blnNumber };

            mapperMock.Setup(_ => _.Map<Farm>(farmDto))
                .Returns(farm);

            // Act
            var result = await sut.AddFarmAsync(farmDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"Farm created with BLN number {farmDto.BlnNumber}", returnValue);
        }

        [Fact]
        public async Task DeleteByBlnNumberAsync_ShouldReturnOk()
        {
            // Act
            var result = await sut.DeleteByBlnNumberAsync(blnNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"Deleted {blnNumber}", returnValue);
        }

        [Fact]
        public async Task DownloadPdfHealthReportAsync_ShouldReturnOk()
        {
            // Arrange
            var fileBytes = new byte[] { 1, 2, 3 };
            var fileName = "test.pdf";
            var expectedFileDownloadDto = new FileDownloadDto
            {
                FileName = fileName,
                ContentType = "application/pdf",
                Content = fileBytes
            };

            farmServiceMock.Setup(_ => _.GeneratePdfHealthReportAsync(blnNumber))
                .ReturnsAsync(fileBytes);

            // Act
            var result = await sut.DownloadPdfHealthReportAsync(blnNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FileDownloadDto>(okResult.Value);
            Assert.Equal(expectedFileDownloadDto.ContentType, returnValue.ContentType);
            Assert.Equal(expectedFileDownloadDto.Content, returnValue.Content);
        }
    }
}
