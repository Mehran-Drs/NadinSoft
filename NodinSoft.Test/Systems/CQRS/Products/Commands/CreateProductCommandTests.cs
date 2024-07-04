using AutoMapper;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Products.Commands.CreateProduct;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;
using System.Linq.Expressions;

namespace NodinSoft.Test.Systems.CQRS.Products.Commands
{
    public class CreateProductCommandTests
    {
        [Fact]
        public async Task CreateProduct_ShouldReturnSuccessful_WhenProductCreatationSucceeds()
        {
            //Arrange 
            var createProductCommand = new CreateProductCommand();
            var result = new Result<int>() { IsSuccessful = true };
            var product = new Product();

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(m => m.Map<Product>(It.IsAny<CreateProductCommand>())).Returns(product);
            mockRepository.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(false);
            mockRepository.Setup(r => r.CreateAsync(It.IsAny<Product>())).ReturnsAsync(true);

            var handle = new CreateProductCommandHandler(mockMapper.Object,mockRepository.Object);

            //Act
            var handler = await handle.Handle(createProductCommand, CancellationToken.None);

            //Assert
            Assert.True(handler.IsSuccessful);
            Assert.Null(handler.Errors);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnUnsuccessful_WhenProductCreatationFails()
        {
            //Arrange 
            var createProductCommand = new CreateProductCommand();
            var result = new Result<int>() { IsSuccessful = false };
            var product = new Product();

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(m => m.Map<Product>(It.IsAny<CreateProductCommand>())).Returns(product);
            mockRepository.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(true);

            var handle = new CreateProductCommandHandler(mockMapper.Object, mockRepository.Object);

            //Act
            var handler = await handle.Handle(createProductCommand, CancellationToken.None);

            //Assert
            Assert.False(handler.IsSuccessful);
            Assert.NotNull(handler.Errors);
            Assert.Single(handler.Errors); // Assuming only one error is returned for duplication
            Assert.Equal("DUPLICATED", handler.Errors.First().ErrorCode); // Assert specific error code
        }
    }
}
