using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class VaccinationSchemeServiceTests
    {
        private readonly VaccinationSchemeService sut;
        private readonly Mock<IVaccinationSchemeRepository> vaccinationSchemeRepositoryMock;
        private readonly Mock<IPathogenRepository> pathogenRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IManufacturerRepository> manufacturerRepositoryMock;

        public VaccinationSchemeServiceTests()
        {
            vaccinationSchemeRepositoryMock = new Mock<IVaccinationSchemeRepository>();
            pathogenRepositoryMock = new Mock<IPathogenRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            manufacturerRepositoryMock = new Mock<IManufacturerRepository>();

            sut = new VaccinationSchemeService(
                vaccinationSchemeRepositoryMock.Object,
                pathogenRepositoryMock.Object,
                productRepositoryMock.Object,
                manufacturerRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenPathogenInvalid()
        {
            // Arrange
            var vaccinationScheme = new VaccinationScheme
            {
                Pathogen = new Pathogen { Name = "Invalid Pathogen" }
            };

            pathogenRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Pathogen, bool>>>()))
                .ReturnsAsync((Pathogen)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.AddAsync(vaccinationScheme));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenProductInvalid()
        {
            // Arrange
            var vaccinationScheme = new VaccinationScheme
            {
                Product = new Product
                {
                    Name = "Invalid Product",
                    Manufacturer = new Manufacturer { Name = "Invalid Manufacturer" }
                },
                Pathogen = new Pathogen
                {
                    Name = "Valid Pathogen"
                }
            };

            productRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync((Product)null);
            manufacturerRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Manufacturer, bool>>>()))
                .ReturnsAsync((Manufacturer)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.AddAsync(vaccinationScheme));
        }

        [Fact]
        public async Task AddAsync_ShouldAddVaccinationScheme_WhenVaccinationSchemeValid()
        {
            // Arrange
            var vaccinationScheme = new VaccinationScheme
            {
                Pathogen = new Pathogen { Name = "Valid Pathogen" },
                Product = new Product { Name = "Valid Product" },
            };
            var dbPathogen = new Pathogen { Name = "Valid Pathogen" };
            var dbProduct = new Product { Name = "Valid Product" };

            pathogenRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Pathogen, bool>>>()))
                .ReturnsAsync(dbPathogen);
            productRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(dbProduct);

            // Act
            await sut.AddAsync(vaccinationScheme);

            // Assert
            vaccinationSchemeRepositoryMock.Verify(_ => _.AddAsync(It.IsAny<VaccinationScheme>()), Times.Once);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldGetVaccinationScheme()
        {
            // Arrange
            var blnNumber = "005630";
            var vaccinationSchemes = new List<VaccinationScheme>();

            vaccinationSchemeRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<VaccinationScheme, bool>>>()))
                .ReturnsAsync(vaccinationSchemes);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            vaccinationSchemeRepositoryMock.Verify(_ => _.GetAsync(It.IsAny<Expression<Func<VaccinationScheme, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetManufacturerDropdownOptionsAsync_ShouldReturnManufacturers()
        {
            // Arrange
            var manufacturers = new List<Manufacturer>();

            manufacturerRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Manufacturer, bool>>>()))
                .ReturnsAsync(manufacturers);

            // Act
            var result = await sut.GetManufacturerDropdownOptionsAsync();

            // Assert
            manufacturerRepositoryMock.Verify(_ => _.GetAsync(It.IsAny<Expression<Func<Manufacturer, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetProductDropdownOptionsAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>();

            productRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            // Act
            var result = await sut.GetProductDropdownOptionsAsync();

            // Assert
            productRepositoryMock.Verify(_ => _.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetPathogenDropdownOptionsAsync_ShouldReturnPathogens()
        {
            // Arrange
            var pathogens = new List<Pathogen>();

            pathogenRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Pathogen, bool>>>()))
                .ReturnsAsync(pathogens);

            // Act
            var result = await sut.GetPathogenDropdownOptionsAsync();

            // Assert
            pathogenRepositoryMock.Verify(_ => _.GetAsync(It.IsAny<Expression<Func<Pathogen, bool>>>()), Times.Once);
        }
    }
}
