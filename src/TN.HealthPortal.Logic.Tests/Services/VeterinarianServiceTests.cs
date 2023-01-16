using Moq;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.Logic.Tests.Services
{
    public class VeterinarianServiceTests
    {
        private readonly VeterinarianService sut;
        private readonly Mock<IVeterinarianRepository> veterinarianRepositoryMock;

        private readonly string veterinarianEmployeeCode = "EmployeeCode";

        public VeterinarianServiceTests()
        {
            veterinarianRepositoryMock = new Mock<IVeterinarianRepository>();

            sut = new VeterinarianService(veterinarianRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnVeterinarian_WhenVeterinarianExists()
        {
            // Arrange
            var veterinarian = new Veterinarian();
            veterinarianRepositoryMock.Setup(_ => _.GetSingleAsync(It.IsAny<Expression<Func<Veterinarian, bool>>>()))
                .ReturnsAsync(veterinarian);

            // Act
            var result = await sut.GetByEmployeeCodeAsync(veterinarianEmployeeCode);

            // Assert
            Assert.Equal(veterinarian, result);
        }

        [Fact]
        public async Task GetByEmployeeCode_ShouldReturnNull_WhenVeterinarianNotExists()
        {
            // Arrange
            veterinarianRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Veterinarian, bool>>>()))
                .ReturnsAsync(new List<Veterinarian>());

            // Act
            var result = await sut.GetByEmployeeCodeAsync(veterinarianEmployeeCode);

            // Assert
            Assert.Null(result);
        }
    }
}
