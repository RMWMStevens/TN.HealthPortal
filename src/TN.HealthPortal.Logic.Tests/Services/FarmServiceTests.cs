using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class FarmServiceTests
    {
        private readonly FarmService sut;
        private readonly Mock<IFarmRepository> farmRepositoryMock;
        private readonly Mock<ICountryRepository> countryRepositoryMock;
        private readonly Mock<IRegionRepository> regionRepositoryMock;

        private readonly string blnNumber = "005630";

        public FarmServiceTests()
        {
            farmRepositoryMock = new Mock<IFarmRepository>();
            countryRepositoryMock = new Mock<ICountryRepository>();
            regionRepositoryMock = new Mock<IRegionRepository>();

            sut = new FarmService(farmRepositoryMock.Object, regionRepositoryMock.Object, countryRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddFarm()
        {
            // Arrange
            var farm = new Farm
            {
                Country = new Country
                {
                    Name = "TestCountry",
                    Region = new Region { Name = "TestRegion" },
                },
            };
            countryRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(new List<Country> { farm.Country });
            regionRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                .ReturnsAsync(new List<Region> { farm.Country.Region });

            // Act
            await sut.AddAsync(farm);

            // Assert
            farmRepositoryMock.Verify(_ => _.AddAsync(It.Is<Farm>(farm => farm.Country == farm.Country)), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenInvalidFarmCountryOrRegion()
        {
            // Arrange
            var farm = new Farm
            {
                Country = new Country
                {
                    Name = "TestCountry",
                    Region = new Region { Name = "TestRegion" },
                },
            };
            countryRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(new List<Country>());
            regionRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Region, bool>>>()))
                .ReturnsAsync(new List<Region>());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.AddAsync(farm));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnFarms_WhenVeterinarianHasMatchingFarms()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            var farms = new List<Farm> { new Farm { Country = new Country { Region = new Region() } } };
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(farms);

            // Act
            var result = await sut.GetAllAsync(veterinarian);

            // Assert
            Assert.Equal(farms, result);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm { BlnNumber = blnNumber };
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(new List<Farm> { farm });

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.Equal(farm, result);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnNull_WhenFarmNotExists()
        {
            // Arrange
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(new List<Farm>());

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteByBlnNumberAsync_ShouldDeleteFarm_WhenFarmExists()
        {
            // Arrange
            var farm = new Farm { BlnNumber = blnNumber };
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(new List<Farm> { farm });

            // Act
            await sut.DeleteByBlnNumberAsync(blnNumber);

            // Assert
            farmRepositoryMock.Verify(_ => _.RemoveAsync(It.Is<Farm>(f => f.BlnNumber == blnNumber)), Times.Once);
        }

        [Fact]
        public async Task DeleteByBlnNumberAsync_ShouldDoNothing_WhenFarmNotExists()
        {
            // Arrange
            farmRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Farm, bool>>>()))
                .ReturnsAsync(new List<Farm>());

            // Act
            await sut.DeleteByBlnNumberAsync(blnNumber);

            // Assert
            farmRepositoryMock.Verify(_ => _.RemoveAsync(It.IsAny<Farm>()), Times.Never);
        }
    }
}
