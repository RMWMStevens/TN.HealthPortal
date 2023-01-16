using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Generators;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class FarmServiceTests
    {
        private readonly FarmService sut;
        private readonly Mock<IFarmRepository> farmRepositoryMock;
        private readonly Mock<IRegionRepository> regionRepositoryMock;
        private readonly Mock<ICountryRepository> countryRepositoryMock;
        private readonly Mock<IFarmExportGenerator> farmExportGeneratorMock;
        private readonly Mock<IVaccinationSchemeRepository> vaccinationSchemeRepositoryMock;
        private readonly Mock<IDewormingSchemeRepository> dewormingSchemeRepositoryMock;

        private readonly string blnNumber = "005630";
        private readonly string blnNumber2 = "005631";

        public FarmServiceTests()
        {
            farmRepositoryMock = new Mock<IFarmRepository>();
            regionRepositoryMock = new Mock<IRegionRepository>();
            countryRepositoryMock = new Mock<ICountryRepository>();
            farmExportGeneratorMock = new Mock<IFarmExportGenerator>();
            vaccinationSchemeRepositoryMock = new Mock<IVaccinationSchemeRepository>();
            dewormingSchemeRepositoryMock = new Mock<IDewormingSchemeRepository>();

            sut = new FarmService(
                farmRepositoryMock.Object,
                regionRepositoryMock.Object,
                countryRepositoryMock.Object,
                vaccinationSchemeRepositoryMock.Object,
                dewormingSchemeRepositoryMock.Object,
                farmExportGeneratorMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddFarm_WhenFarmValid()
        {
            // Arrange
            var farm = new Farm
            {
                BlnNumber = blnNumber,
                Country = new Country { Name = "Test Country" },
                Veterinarians = new List<Veterinarian>()
            };

            var dbCountry = new Country { Name = "Test Country" };
            var dbRegion = new Region { Name = "Test Region" };

            countryRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(dbCountry);
            regionRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                .ReturnsAsync(dbRegion);
            farmRepositoryMock.Setup(_ => _.AddAsync(It.IsAny<Farm>())).Returns(Task.CompletedTask);

            // Act
            await sut.AddAsync(farm);

            // Assert
            farmRepositoryMock.Verify(_ => _.AddAsync(It.Is<Farm>(farm => farm.BlnNumber == blnNumber && farm.Country == dbCountry)), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenCountryOrRegionInvalid()
        {
            // Arrange
            var farm = new Farm
            {
                BlnNumber = blnNumber,
                Country = new Country
                {
                    Name = "Invalid Country",
                    Region = new Region()
                    {
                        Name = "Invalid Region"
                    }
                },
                Veterinarians = new List<Veterinarian>()
            };

            countryRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync((Country)null);
            regionRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                .ReturnsAsync((Region)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.AddAsync(farm));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnFarms_WhenVeterinarianValid()
        {
            // Arrange
            var veterinarian = new Veterinarian { Countries = new List<Country>() };
            var farms = new List<Farm> { new Farm { BlnNumber = blnNumber }, new Farm { BlnNumber = blnNumber2 } };

            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(farms);

            // Act
            var result = await sut.GetAllAsync(veterinarian);

            // Assert
            Assert.Equal(farms, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldThrowArgumentException_WhenVeterinarianInvalid()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetAllAsync(null));
        }

        [Fact]
        public async Task GetAllOutdatedAsync_ShouldReturnOutdatedFarms_WhenFarmExistsWithManuallyUpdatedAtOlderThanAYear()
        {
            // Arrange
            var veterinarian = new Veterinarian { Countries = new List<Country>() };
            var farms = new List<Farm>
            {
                new Farm { BlnNumber = blnNumber, ManuallyUpdatedAt = DateTime.UtcNow },
                new Farm { BlnNumber = blnNumber2, ManuallyUpdatedAt = DateTime.UtcNow.AddYears(-2) }
            };
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>())).ReturnsAsync(farms);

            // Act
            var result = await sut.GetAllOutdatedAsync(veterinarian);

            // Assert
            Assert.Single(result);
            Assert.Equal(blnNumber2, result.First().BlnNumber);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm { BlnNumber = blnNumber };
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(farm);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.Equal(farm, result);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnNull_WhenFarmNotExists()
        {
            // Arrange
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync((Farm)null);

            // Act
            var result = await sut.GetByBlnNumberAsync("Invalid BLN Number");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteByBlnNumberAsync_ShouldDeleteFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm { BlnNumber = blnNumber };
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(farm);
            farmRepositoryMock.Setup(_ => _.RemoveAsync(It.IsAny<Farm>()))
                .Returns(Task.CompletedTask);

            // Act
            await sut.DeleteByBlnNumberAsync(blnNumber);

            // Assert
            farmRepositoryMock.Verify(_ => _.RemoveAsync(It.Is<Farm>(farm => farm.BlnNumber == blnNumber)), Times.Once);
        }

        [Fact]
        public async Task DeleteByBlnNumberAsync_ShouldThrowArgumentException_WhenFarmNotExists()
        {
            // Arrange
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync((Farm)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.DeleteByBlnNumberAsync("Invalid BLN Number"));
        }

        [Fact]
        public async Task GeneratePdfHealthReportAsync_ShouldGeneratePdfReport_WhenFarmExists()
        {
            // Arrange
            var fileBytes = new byte[] { 1, 2, 3 };
            var farm = new Farm { BlnNumber = blnNumber };
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(farm);
            dewormingSchemeRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<DewormingScheme, bool>>>()))
                .ReturnsAsync(new List<DewormingScheme>());
            vaccinationSchemeRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<VaccinationScheme, bool>>>()))
                .ReturnsAsync(new List<VaccinationScheme>());
            farmExportGeneratorMock.Setup(_ => _.Generate(It.IsAny<Farm>()))
                .Returns(fileBytes);

            // Act
            var result = await sut.GeneratePdfHealthReportAsync(blnNumber);

            // Assert
            farmExportGeneratorMock.Verify(_ => _.Generate(It.Is<Farm>(farm => farm.BlnNumber == blnNumber)), Times.Once);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GeneratePdfHealthReportAsync_ShouldThrowArgumentException_WhenFarmNotExists()
        {
            // Arrange
            farmRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync((Farm)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GeneratePdfHealthReportAsync("Invalid BLN Number"));
        }
    }
}
