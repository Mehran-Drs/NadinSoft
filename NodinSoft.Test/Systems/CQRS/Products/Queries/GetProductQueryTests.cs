using AutoMapper;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;

namespace NodinSoft.Test.Systems.CQRS.Products.Queries
{
    public class GetProductQueryTests
    {
        [Fact]
        public async Task GetProduct_ShouldReturnSuccseeful_WhenGetProductSucceeded()
        {
            //Arrange
            var getProductQuery = new GetProductQuery();
            var result = new Result<GetProductQueryResult>() { IsSuccessful = true };
            var product = new Product();
            var getProductQueryResult = new GetProductQueryResult();

            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            mockMapper.Setup(m => m.Map<GetProductQueryResult>(It.IsAny<Product>())).Returns(getProductQueryResult);

            var handle = new GetProductQueryHandler(mockMapper.Object, mockRepository.Object);

            //Act
            var handler = await handle.Handle(getProductQuery, CancellationToken.None);

            //Assert
            Assert.True(handler.IsSuccessful);
            Assert.Equal(handler.IsSuccessful, result.IsSuccessful);
            Assert.Null(handler.Errors);
        }
    }
}
