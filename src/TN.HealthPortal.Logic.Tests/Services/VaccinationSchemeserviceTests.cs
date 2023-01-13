using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class VaccinationSchemeserviceTests
    {
        private readonly VaccinationSchemeService sut;
        private readonly Mock<IVaccinationSchemeRepository> vaccinationSchemeRepositoryMock;
        private readonly Mock<IPathogenRepository> pathogenRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;

        public VaccinationSchemeserviceTests()
        {
            vaccinationSchemeRepositoryMock = new Mock<IVaccinationSchemeRepository>();
            pathogenRepositoryMock = new Mock<IPathogenRepository>();
            productRepositoryMock = new Mock<IProductRepository>();

            sut = new VaccinationSchemeService(vaccinationSchemeRepositoryMock.Object, pathogenRepositoryMock.Object, productRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddVaccinationScheme()
        {
            // Arrange
            var vaccinationScheme = new VaccinationScheme
            {
                Pathogen = new Pathogen { Name = "Test Pathogen" },
                Product = new Product { Name = "Test Product" }
            };

            pathogenRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Pathogen, bool>>>()))
                .ReturnsAsync(new List<Pathogen> { vaccinationScheme.Pathogen });
            productRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> { vaccinationScheme.Product });

            // Act
            await sut.AddAsync(vaccinationScheme);

            // Assert
            vaccinationSchemeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<VaccinationScheme>()), Times.Once);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldGetVaccinationScheme()
        {
            // Arrange
            var blnNumber = "005630";

            vaccinationSchemeRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<VaccinationScheme, bool>>>()))
                .ReturnsAsync(new List<VaccinationScheme> { new VaccinationScheme { FarmBlnNumber = blnNumber } });

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(blnNumber, result.First().FarmBlnNumber);
        }
    }
}
