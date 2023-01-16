using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TN.HealthPortal.API.Controllers;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.DTOs.DropdownOptions;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;
using Xunit;

namespace TN.HealthPortal.API.Tests.Controllers
{
    public class DewormingSchemesControllerTests
    {
        private readonly DewormingSchemesController sut;
        private readonly Mock<IDewormingSchemeService> dewormingSchemeServiceMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly string blnNumber = "005630";

        public DewormingSchemesControllerTests()
        {
            dewormingSchemeServiceMock = new Mock<IDewormingSchemeService>();
            mapperMock = new Mock<IMapper>();

            sut = new DewormingSchemesController(dewormingSchemeServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetByBlnNumberAsync_ShouldReturnOk_WhenDewormingSchemesExists()
        {
            // Arrange
            var dewormingSchemes = new List<DewormingScheme> { new DewormingScheme() };
            var dewormingSchemesDto = new List<DewormingSchemeDto> { new DewormingSchemeDto() };

            dewormingSchemeServiceMock.Setup(_ => _.GetByBlnNumberAsync(blnNumber))
                .ReturnsAsync(dewormingSchemes);

            mapperMock.Setup(_ => _.Map<IEnumerable<DewormingSchemeDto>>(dewormingSchemes))
                .Returns(dewormingSchemesDto);

            // Act
            var result = await sut.GetByBlnNumberAsync(blnNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DewormingSchemeDto>>(okResult.Value);
            Assert.Equal(dewormingSchemesDto, returnValue);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnOk()
        {
            // Arrange
            var dewormingSchemeDto = new DewormingSchemeDto { FarmBlnNumber = blnNumber };
            var dewormingScheme = new DewormingScheme { FarmBlnNumber = blnNumber };

            mapperMock.Setup(_ => _.Map<DewormingScheme>(dewormingSchemeDto))
                .Returns(dewormingScheme);

            // Act
            var result = await sut.AddAsync(dewormingSchemeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"Deworming scheme created for farm with BlnNumber {dewormingSchemeDto.FarmBlnNumber}", returnValue);
        }

        [Fact]
        public async Task GetDropdownOptionsAsync_ShouldReturnOk()
        {
            // Arrange
            var products = new List<Product> { new Product() };
            var manufacturers = new List<Manufacturer> { new Manufacturer() };

            var productsDto = new List<ProductDto> { new ProductDto() };
            var manufacturersDto = new List<ManufacturerDto> { new ManufacturerDto() };

            var expectedDropdownOptionsDto = new DewormingSchemeDropdownOptionsDto
            {
                Products = productsDto,
                Manufacturers = manufacturersDto
            };

            dewormingSchemeServiceMock.Setup(_ => _.GetProductDropdownOptionsAsync())
                .ReturnsAsync(products);
            dewormingSchemeServiceMock.Setup(_ => _.GetManufacturerDropdownOptionsAsync())
                .ReturnsAsync(manufacturers);

            mapperMock.Setup(_ => _.Map<IEnumerable<ProductDto>>(products))
                .Returns(productsDto);
            mapperMock.Setup(_ => _.Map<IEnumerable<ManufacturerDto>>(manufacturers))
                .Returns(manufacturersDto);

            // Act
            var result = await sut.GetDropdownOptionsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DewormingSchemeDropdownOptionsDto>(okResult.Value);
            Assert.Equal(expectedDropdownOptionsDto.Products, returnValue.Products);
            Assert.Equal(expectedDropdownOptionsDto.Manufacturers, returnValue.Manufacturers);
        }
    }
}
