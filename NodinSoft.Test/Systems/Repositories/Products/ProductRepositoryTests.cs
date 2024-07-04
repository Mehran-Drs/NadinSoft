using Moq;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;

namespace NodinSoft.Test.Systems.Repositories.Products
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        public ProductRepositoryTests()
        {
            _mockRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnSuccessful()
        {
            //Arrange 
            var product = new Product();
            var result = true;

            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Product>())).ReturnsAsync(result);

            //Act
            var createMethod = await _mockRepository.Object.CreateAsync(product);

            //Assert
            Assert.Equal(result, createMethod);
            Assert.True(createMethod);
        }

        [Fact]
        public async Task EditProduct_ShouldReturnSuccessful()
        {
            //Arrange 
            var product = new Product();
            var result = true;

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(result);

            //Act
            var editMethod = await _mockRepository.Object.UpdateAsync(product);

            //Assert
            Assert.Equal(result, editMethod);
            Assert.True(editMethod);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnSuccessful()
        {
            //Arrange 
            var product = new Product();
            var result = true;

            _mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Product>())).ReturnsAsync(result);

            //Act
            var deleteMethod = await _mockRepository.Object.DeleteAsync(product);

            //Assert
            Assert.Equal(result, deleteMethod);
            Assert.True(deleteMethod);
        }
    }
}
