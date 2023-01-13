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
    public class VaccinationSchemesControllerTests
    {
        private readonly VaccinationSchemesController sut;
        private readonly Mock<IVaccinationSchemeService> mockVaccinationSchemeService;
        private readonly Mock<IMapper> mockMapper;

        private readonly string blnNumber = "005630";

        public VaccinationSchemesControllerTests()
        {
            mockVaccinationSchemeService = new Mock<IVaccinationSchemeService>();
            mockMapper = new Mock<IMapper>();

            sut = new VaccinationSchemesController(mockVaccinationSchemeService.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOkResultWithVaccinationSchemes_WhenVaccinationSchemesExist()
        {
            // Arrange
            var vaccinationSchemes = new List<VaccinationScheme> { new VaccinationScheme(), new VaccinationScheme() };
            mockVaccinationSchemeService
                .Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(vaccinationSchemes);

            var expected = new List<VaccinationSchemeDto> { new VaccinationSchemeDto(), new VaccinationSchemeDto() };
            mockMapper
                .Setup(_ => _.Map<IEnumerable<VaccinationSchemeDto>>(vaccinationSchemes))
                .Returns(expected);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnNotFoundResult_WhenVaccinationSchemesNotFound()
        {
            // Arrange
            mockVaccinationSchemeService
                .Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ThrowsAsync(new Exception());

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal($"No vaccination schemes found with BLN number {blnNumber}", notFoundResult.Value);
        }

        [Fact]
        public async Task AddVaccinationSchemeAsync_ShouldReturnOkResultWithVaccinationSchemeProductName_WhenVaccinationSchemeAdded()
        {
            // Arrange
            var vaccinationSchemeDto = new VaccinationSchemeDto { FarmBlnNumber = blnNumber };
            var vaccinationScheme = new VaccinationScheme();
            mockMapper
                .Setup(_ => _.Map<VaccinationScheme>(vaccinationSchemeDto))
                .Returns(vaccinationScheme);

            // Act
            var result = await sut.AddVaccinationSchemeAsync(vaccinationSchemeDto);

            // Assert
            mockVaccinationSchemeService.Verify(_ => _.AddAsync(vaccinationScheme), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal($"Vaccination scheme created for farm with BlnNumber {blnNumber}", okResult.Value);
        }
    }
}
