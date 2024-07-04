using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NadinSoft.API.Controllers;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Products.Commands.CreateProduct;
using NadinSoft.Application.CQRS.Products.Commands.DeleteProduct;
using NadinSoft.Application.CQRS.Products.Commands.EditProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProductsList;
using NadinSoft.Application.Services.Authentication.JwtServices;
using NadinSoft.Common.DTOs;
using System.Security.Claims;

namespace NodinSoft.Test.Systems.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IJwtService> _mockJwt;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockJwt = new Mock<IJwtService>();
            _mockMediator = new Mock<IMediator>();
            _controller = new ProductsController(_mockMediator.Object, _mockJwt.Object);
        }

        #region GetProductById
        [Fact]
        public async Task GetProductById_ShouldReturnOk_WhenSuccessful()
        {
            //Arrange 
            var productId = 1;
            var getProductQuery = new GetProductQuery { ProductId = productId };
            var result = new Result<GetProductQueryResult>() { IsSuccessful = true };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default)).ReturnsAsync(result);

            //Act
            var getProductByIdAction = await _controller.GetProductById(productId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(getProductByIdAction);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange
            var productId = 1;
            var getProductQuery = new GetProductQuery { ProductId = productId };
            var result = new Result<GetProductQueryResult>() { IsSuccessful = false };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default)).ReturnsAsync(result);

            //Act
            var getProductByIdAction = await _controller.GetProductById(productId);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(getProductByIdAction);
            Assert.Equal(result, badRequestResult.Value);
        }
        #endregion

        #region GetProducts
        [Fact]
        public async Task GetProducts_ShouldReturnOk_WhenSuccessful()
        {
            //Arrange 
            var page = 1;
            var limit = 20;
            string firstName = null;
            string lastName = null;
            var getProductsListQuery = new GetProductsListQuery()
            {
                FirstName = firstName,
                LastName = lastName,
                Limit = limit,
                Page = page
            };

            var result = new Result<PaginationDto<GetProductsListResult>>() { IsSuccessful = true };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductsListQuery>(), default)).ReturnsAsync(result);

            //Act
            var getProductsAction = await _controller.GetProducts(page, limit, firstName, lastName);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(getProductsAction);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange 
            var page = 1;
            var limit = 20;
            string firstName = null;
            string lastName = null;
            var getProductsListQuery = new GetProductsListQuery()
            {
                FirstName = firstName,
                LastName = lastName,
                Limit = limit,
                Page = page
            };

            var result = new Result<PaginationDto<GetProductsListResult>>() { IsSuccessful = false };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductsListQuery>(), default)).ReturnsAsync(result);

            //Act
            var getProductsAction = await _controller.GetProducts(page, limit, firstName, lastName);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(getProductsAction);
            Assert.Equal(result, badRequestResult.Value);
        }
        #endregion

        #region CreateProduct
        [Fact]
        public async Task CreateProduct_ShouldReturnCreated_WhenSuccessful()
        {
            //Arrange 
            var createProductCommand = new CreateProductCommand();
            var result = new Result<int>() { Value = 1, IsSuccessful = true };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";

            _mockJwt.Setup(m => m.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };

            _controller.HttpContext.Request.Headers["Authorization"] = token;

            //Act
            var createProductAction = await _controller.CreateProduct(createProductCommand);

            //Assert
            var createdResult = Assert.IsType<CreatedResult>(createProductAction);
            Assert.Equal($"/api/products/getproductbyid/{result.Value}", createdResult.Location);
            Assert.Equal(result, createdResult.Value);
        }


        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange 
            var createProductCommand = new CreateProductCommand();
            var result = new Result<int>() { IsSuccessful = false };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";
            _mockJwt.Setup(j => j.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            _controller.HttpContext.Request.Headers.Authorization = token;

            //Act
            var createProductAction = await _controller.CreateProduct(createProductCommand);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(createProductAction);
            Assert.Equal(result, badRequestResult.Value);
        }

        #endregion

        #region EditProduct
        [Fact]
        public async Task EditProduct_ShouldReturnOk_WhenSuccessful()
        {
            //Arrange 
            var editProductCommand = new EditProductCommand();
            var result = new Result<bool>() { IsSuccessful = true };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";
            _mockJwt.Setup(j => j.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<EditProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            _controller.HttpContext.Request.Headers.Authorization = token;


            //Act
            var editProductAction = await _controller.EditProduct(editProductCommand);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(editProductAction);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task EditProduct_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange 
            var editProductCommand = new EditProductCommand();

            var result = new Result<bool>() { IsSuccessful = false };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";
            _mockJwt.Setup(j => j.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<EditProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            _controller.HttpContext.Request.Headers.Authorization = token;


            //Act
            var editProductAction = await _controller.EditProduct(editProductCommand);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(editProductAction);
            Assert.Equal(result, badRequestResult.Value);
        }

        #endregion

        #region DeleteProduct
        [Fact]
        public async Task DeleteProduct_ShouldReturnOk_WhenSuccessful()
        {
            //Arrange 
            var productId = 1;
            var deleteProductCommand = new DeleteProductCommand() { ProductId = productId };
            var result = new Result<bool>() { IsSuccessful = true };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";
            _mockJwt.Setup(j => j.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            _controller.HttpContext.Request.Headers.Authorization = token;


            //Act
            var deleteProductAction = await _controller.DeleteProduct(deleteProductCommand);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(deleteProductAction);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange 
            var productId = 1;
            var deleteProductCommand = new DeleteProductCommand() { ProductId = productId};

            var result = new Result<bool>() { IsSuccessful = false };
            var claims = new List<Claim>() { new Claim("id", "1") };
            var token = "Bearer";
            _mockJwt.Setup(j => j.GetClaims(token)).Returns(claims);
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            _controller.HttpContext.Request.Headers.Authorization = token;


            //Act
            var deleteProductAction = await _controller.DeleteProduct(deleteProductCommand);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(deleteProductAction);
            Assert.Equal(result, badRequestResult.Value);
        }

        #endregion
    }
}
