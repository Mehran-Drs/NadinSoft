using AutoMapper;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Products.Commands.DeleteProduct;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;
using System.Linq.Expressions;

namespace NodinSoft.Test.Systems.CQRS.Products.Commands
{
    public class DeleteProductCommandTests
    {

        [Fact]
        public async Task DeleteProduct_ShouldReturnSuccessful_WhenUpdateSucceeded()
        {
            // Arrange
            var command = new DeleteProductCommand();

            var product = new Product();

            var expectedResult = new Result<bool>() { IsSuccessful = true };

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Product>())).ReturnsAsync(true);


            var handler = new DeleteProductCommandHandler(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
            Assert.Null(result.Errors);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnUnsuccessful_WhenUserNotAllowed()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                ProductId = 1,
                CreatorId = 2,
            };

            var product = new Product
            {
                Id = 1,
                CreatorId = 1,
            };

            var expectedResult = new Result<bool>(false, false, new List<Error> { new Error("NOTALLOWED", "User Can Not Delete This Item") });

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var handler = new DeleteProductCommandHandler(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
            Assert.NotNull(result.Errors);
            Assert.Single(result.Errors); // Assuming only one error is returned
            Assert.Equal(expectedResult.Errors.First().ErrorCode, result.Errors.First().ErrorCode);
            Assert.Equal(expectedResult.Errors.First().ErrorMessage, result.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnUnuccessful_WhenServerErrorOccured()
        {
            // Arrange
            var command = new DeleteProductCommand();

            var product = new Product();

            var expectedResult = new Result<bool>(false, false, new List<Error> { new Error("SERVERERROR", "Something Went Wrong ...") });

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Product>())).ReturnsAsync(false); // Update fails

            mockMapper.Setup(m => m.Map(It.IsAny<DeleteProductCommand>(), It.IsAny<Product>())).Returns(product);

            var handler = new DeleteProductCommandHandler(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
            Assert.NotNull(result.Errors);
            Assert.Single(result.Errors); // Assuming only one error is returned
            Assert.Equal(expectedResult.Errors.First().ErrorCode, result.Errors.First().ErrorCode);
            Assert.Equal(expectedResult.Errors.First().ErrorMessage, result.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnUnuccessful_WhenProductNotFound()
        {
            // Arrange
            var command = new DeleteProductCommand();

            Product product = null; // Product not found

            var expectedResult = new Result<bool>(false, false, new List<Error> { new Error("NOTFOUND", "Product Could Not Be Found") });

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var handler = new DeleteProductCommandHandler(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(expectedResult.IsSuccessful, result.IsSuccessful);
            Assert.NotNull(result.Errors);
            Assert.Single(result.Errors); // Assuming only one error is returned
            Assert.Equal(expectedResult.Errors.First().ErrorCode, result.Errors.First().ErrorCode);
            Assert.Equal(expectedResult.Errors.First().ErrorMessage, result.Errors.First().ErrorMessage);
        }
    }
}
