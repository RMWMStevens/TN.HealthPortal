using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class DewormingSchemeServiceTests
    {
        private readonly DewormingSchemeService sut;
        private readonly Mock<IDewormingSchemeRepository> dewormingSchemeRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IManufacturerRepository> manufacturerRepositoryMock;

        public DewormingSchemeServiceTests()
        {
            dewormingSchemeRepositoryMock = new Mock<IDewormingSchemeRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            manufacturerRepositoryMock = new Mock<IManufacturerRepository>();

            sut = new DewormingSchemeService(
                dewormingSchemeRepositoryMock.Object,
                productRepositoryMock.Object,
                manufacturerRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenProductInvalid()
        {
            // Arrange
            var dewormingScheme = new DewormingScheme
            {
                Product = new Product
                {
                    Name = "Invalid Product",
                    Manufacturer = new Manufacturer { Name = "Invalid Manufacturer" }
                }
            };

            productRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync((Product)null);
            manufacturerRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Manufacturer, bool>>>()))
                .ReturnsAsync((Manufacturer)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.AddAsync(dewormingScheme));
        }

        [Fact]
        public async Task AddAsync_ShouldAddDewormingScheme_WhenDewormingSchemeValid()
        {
            // Arrange
            var dewormingScheme = new DewormingScheme
            {
                Product = new Product { Name = "Valid Product" },
            };
            var dbPathogen = new Pathogen { Name = "Valid Pathogen" };
            var dbProduct = new Product { Name = "Valid Product" };

            productRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(dbProduct);

            // Act
            await sut.AddAsync(dewormingScheme);

            // Assert
            dewormingSchemeRepositoryMock.Verify(_ => _.AddAsync(It.IsAny<DewormingScheme>()), Times.Once);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldGetDewormingScheme()
        {
            // Arrange
            var blnNumber = "005630";
            var dewormingSchemes = new List<DewormingScheme>();

            dewormingSchemeRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<DewormingScheme, bool>>>()))
                .ReturnsAsync(dewormingSchemes);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            dewormingSchemeRepositoryMock.Verify(_ => _.GetAsync(It.IsAny<Expression<Func<DewormingScheme, bool>>>()), Times.Once);
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
    }
}
