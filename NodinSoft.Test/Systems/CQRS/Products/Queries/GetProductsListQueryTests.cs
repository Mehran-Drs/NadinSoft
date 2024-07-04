using AutoMapper;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Products.Queries.GetProductsList;
using NadinSoft.Common.DTOs;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;
using System.Linq.Expressions;

namespace NodinSoft.Test.Systems.CQRS.Products.Queries
{
    public class GetProductsListQueryTests
    {
        [Fact]
        public async Task GetProductsList_ShouldReturnSuccseeful_WhenGetProductSucceeded()
        {
            //Arrange
            var getProductsListQuery = new GetProductsListQuery();
            var result = new Result<PaginationDto<GetProductsListQuery>>() { IsSuccessful = true };
            var getProductsListQueryResult = new GetProductsListQuery();
            var products = new List<Product>().AsQueryable();
            var productsResult = new List<GetProductsListResult>().AsQueryable();
            var mockRepository = new Mock<IProductRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepository.Setup(r => r.AsQueryable(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<Expression<Func<Product, object>>[]>()))
                      .Returns(products);
            mockMapper.Setup(m => m.ProjectTo<GetProductsListResult>(It.IsAny<IQueryable<Product>>(), It.IsAny<object[]>()))
            .Returns(productsResult);

            var handle = new GetProductsListQueryHandler(mockRepository.Object, mockMapper.Object);

            //Act
            var handler = await handle.Handle(getProductsListQuery, CancellationToken.None);

            //Assert
            Assert.True(handler.IsSuccessful);
            Assert.Equal(handler.IsSuccessful, result.IsSuccessful);
            Assert.Null(handler.Errors);
        }
    }
}
