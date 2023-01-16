using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.DropdownOptions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.API.Tests
{
    public class VaccinationSchemesControllerTests
    {
        private readonly VaccinationSchemesController sut;
        private readonly Mock<IVaccinationSchemeService> vaccinationSchemeServiceMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly string blnNumber = "005630";

        public VaccinationSchemesControllerTests()
        {
            vaccinationSchemeServiceMock = new Mock<IVaccinationSchemeService>();
            mapperMock = new Mock<IMapper>();

            sut = new VaccinationSchemesController(vaccinationSchemeServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOk_WhenVaccinationSchemesExists()
        {
            // Arrange
            var vaccinationSchemes = new List<VaccinationScheme> { new VaccinationScheme() { Pathogen = new() } };
            var vaccinationSchemesDto = new List<VaccinationSchemeDto> { new VaccinationSchemeDto() };

            vaccinationSchemeServiceMock.Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(vaccinationSchemes);
            mapperMock.Setup(_ => _.Map<IEnumerable<VaccinationSchemeDto>>(vaccinationSchemes))
                .Returns(vaccinationSchemesDto);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<VaccinationSchemeDto>>(okResult.Value);
            Assert.Equal(vaccinationSchemesDto, returnValue);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnOk()
        {
            // Arrange
            var vaccinationSchemeDto = new VaccinationSchemeDto { FarmBlnNumber = blnNumber };
            var vaccinationScheme = new VaccinationScheme { FarmBlnNumber = blnNumber };

            mapperMock.Setup(_ => _.Map<VaccinationScheme>(vaccinationSchemeDto))
                .Returns(vaccinationScheme);

            // Act
            var result = await sut.AddAsync(vaccinationSchemeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"Vaccination scheme created for farm with BlnNumber {vaccinationSchemeDto.FarmBlnNumber}", returnValue);
        }

        [Fact]
        public async Task GetDropdownOptionsAsync_ShouldReturnOk()
        {
            // Arrange
            var pathogens = new List<Pathogen> { new Pathogen() };
            var products = new List<Product> { new Product() };
            var manufacturers = new List<Manufacturer> { new Manufacturer() };

            var pathogensDto = new List<PathogenDto> { new PathogenDto() };
            var productsDto = new List<ProductDto> { new ProductDto() };
            var manufacturersDto = new List<ManufacturerDto> { new ManufacturerDto() };

            var expectedDropdownOptionsDto = new VaccinationSchemeDropdownOptionsDto
            {
                Pathogens = pathogensDto,
                Products = productsDto,
                Manufacturers = manufacturersDto
            };

            vaccinationSchemeServiceMock.Setup(_ => _.GetPathogenDropdownOptionsAsync())
                .ReturnsAsync(pathogens);
            vaccinationSchemeServiceMock.Setup(_ => _.GetProductDropdownOptionsAsync())
                .ReturnsAsync(products);
            vaccinationSchemeServiceMock.Setup(_ => _.GetManufacturerDropdownOptionsAsync())
                .ReturnsAsync(manufacturers);

            mapperMock.Setup(_ => _.Map<IEnumerable<PathogenDto>>(pathogens))
                .Returns(pathogensDto);
            mapperMock.Setup(_ => _.Map<IEnumerable<ProductDto>>(products))
                .Returns(productsDto);
            mapperMock.Setup(_ => _.Map<IEnumerable<ManufacturerDto>>(manufacturers))
                .Returns(manufacturersDto);

            // Act
            var result = await sut.GetDropdownOptionsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<VaccinationSchemeDropdownOptionsDto>(okResult.Value);
            Assert.Equal(expectedDropdownOptionsDto.Pathogens, returnValue.Pathogens);
            Assert.Equal(expectedDropdownOptionsDto.Products, returnValue.Products);
            Assert.Equal(expectedDropdownOptionsDto.Manufacturers, returnValue.Manufacturers);
        }
    }
}
