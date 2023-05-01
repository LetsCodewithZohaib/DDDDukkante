using System;
using System.Threading.Tasks;
using AutoMapper;
using Dukkantek.Inventory.Api.Controllers;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Application.Products.Queries;
using Dukkantek.Inventory.Common.Enums;
using Dukkantek.Inventory.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dukkantek.Inventory.Api.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductsController>>();

            _controller = new ProductsController(_mockMediator.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ChangeProductStatus_Returns_CreatedAtRouteResult_With_GetProductQueryResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var command = new ChangeProductStatusCommand
            {
                Id = productId,
                Status = ProductStatusEnum.InStock
            };
            var expectedResult = new GetProductQueryResult
            {
                Id = productId,
                Name = "Product Name",
                BarCode = "1234567890123",
                Description = "Product Description",
                Weight = 1.5m,
                Status = ProductStatusEnum.InStock,
                CategoryId = Guid.NewGuid(),
                CategoryName = "Category Name"
            };
            _mockMediator.Setup(x => x.Send(command, default)).ReturnsAsync(expectedResult);

            var expectedRouteName = nameof(ProductsController.GetProduct);
            var expectedRouteValues = new { productId };

            _mockMapper.Setup(m => m.Map<GetProductQueryResult>(It.IsAny<Product>())).Returns(expectedResult);

            // Act
            var result = await _controller.ChangeProductStatus(command);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(expectedResult, createdAtRouteResult.Value);
            Assert.Equal(expectedRouteName, createdAtRouteResult.RouteName);
            
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            var products = new List<GetProductQueryResult>
    {
        new GetProductQueryResult { Id = Guid.NewGuid(), Name = "Product1" },
        new GetProductQueryResult { Id = Guid.NewGuid(), Name = "Product2" }
    };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GetProductQueryResult>>(okResult.Value);
            Assert.Equal(products.Count, model.Count());
        }

        [Fact]
        public async Task SellProduct_ReturnsCreatedAtRouteResultWithCreatedProduct()
        {
            // Arrange
            var command = new SellProductCommand { Id = Guid.NewGuid() };
            var createdProduct = new GetProductQueryResult { Id = Guid.NewGuid(), Name = "Product1" };
            _mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(createdProduct);
            _mockMapper.Setup(m => m.Map<GetProductQueryResult>(command)).Returns(createdProduct);

            // Act
            var result = await _controller.SellProduct(command);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(nameof(ProductsController.GetProduct), createdAtRouteResult.RouteName);
            Assert.Equal(createdProduct.Id, createdAtRouteResult.RouteValues["productid"]);
            var model = Assert.IsAssignableFrom<GetProductQueryResult>(createdAtRouteResult.Value);
            Assert.Equal(createdProduct.Id, model.Id);
            Assert.Equal(createdProduct.Name, model.Name);
        }

        [Fact]
        public async Task GetProductsCount_ReturnsOkWithProductCount()
        {
            // Arrange
            var expectedProductCount = new List<GetProductCountQueryResult>()
            {
                new GetProductCountQueryResult() { Sold = 10 }
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetProductCountQueryResult>(), default))
                .ReturnsAsync(expectedProductCount);

            var mapperMock = new Mock<IMapper>();

            var loggerMock = new Mock<ILogger<ProductsController>>();

            var controller = new ProductsController(mediatorMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetProductsCount();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualProductCount = Assert.IsAssignableFrom<IEnumerable<GetProductCountQueryResult>>(okResult.Value);
            Assert.Equal(expectedProductCount, actualProductCount);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }



    }
}
