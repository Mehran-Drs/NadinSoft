using Microsoft.EntityFrameworkCore;
using Moq;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;
using System.Linq.Expressions;

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

        [Fact]
        public async Task GetProductById_ShouldReturnProduct()
        {
            //Arrange 
            var product = new Product();
            var productId = 1;

            _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //Act
            var getProductByIdMethod = await _mockRepository.Object.GetByIdAsync(productId);

            //Assert
            Assert.Equal(product, getProductByIdMethod);
            Assert.NotNull(getProductByIdMethod);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNull()
        {
            //Arrange 
            Product product = null;
            var productId = 1;

            _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //Act
            var getProductByIdMethod = await _mockRepository.Object.GetByIdAsync(productId);

            //Assert
            Assert.Equal(product, getProductByIdMethod);
            Assert.Null(getProductByIdMethod);
        }

        [Fact]
        public async Task AsQueryable_ShouldReturnIQueryableProducts_WhenBothParametersNull()
        {
            //Arrange 
            var products = new List<Product>().AsQueryable();

            _mockRepository.Setup(r => r.AsQueryable(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()))
                .Returns(products);

            //Act
            var asQueryableMethod = _mockRepository.Object.AsQueryable();

            //Assert
            Assert.Equal(products, asQueryableMethod);
            Assert.NotNull(asQueryableMethod);
        }

        [Fact]
        public async Task AsQueryable_ShouldReturnFilteredProducts_WhenFilterNotNull()
        {
            // Arrange
            Expression<Func<Product, bool>> filter =  p => p.CreatorId == 1;
            var products = new List<Product>()
            {
                new Product()
                {
                    Id= 1,
                    Name = "test",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now,
                    CreatorId=1,
                    IsAvailable =true,
                    ManufactureEmail = "Test@gmail.com",
                    ManufacturePhone = "11111111111",
                },
                  new Product()
                {
                    Id= 2,
                    Name = "test2",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now.AddDays(1),
                    CreatorId=3,
                    IsAvailable =false,
                    ManufactureEmail = "Test1@gmail.com",
                    ManufacturePhone = "22222222222",
                },
            }
                .AsQueryable();

            _mockRepository.Setup(r => r.AsQueryable(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()))
                .Returns((Expression<Func<Product, bool>> filter, Expression<Func<Product, object>>[] includes) =>
                {
                    IQueryable<Product> query = products;

                    if (includes != null)
                    {
                        foreach (var include in includes)
                        {
                            query = query.Include(include);
                        }
                    }

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    return query;
                });

            // Act
            var filteredProducts = _mockRepository.Object.AsQueryable(filter);

            // Assert
            Assert.Equal(1, filteredProducts.Count()); 
            Assert.All(filteredProducts, p => Assert.True(p.CreatorId == 1)); 
        }

        [Fact]
        public async Task AsQueryable_ShouldReturnFilteredProducts_WhenIncludesNotNull()
        {
            // Arrange
            var includes = new Expression<Func<Product, object>>[]
                 {
                     p => p.Creator,
                 };
            var products = new List<Product>()
            {
                new Product()
                {
                    Id= 1,
                    Name = "test",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now,
                    CreatorId=1,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =1},
                    IsAvailable =true,
                    ManufactureEmail = "Test@gmail.com",
                    ManufacturePhone = "11111111111",
                },
                  new Product()
                {
                    Id= 2,
                    Name = "test2",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now.AddDays(1),
                    CreatorId=3,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =3},
                    IsAvailable =false,
                    ManufactureEmail = "Test1@gmail.com",
                    ManufacturePhone = "22222222222",
                },
            }
                .AsQueryable();

            _mockRepository.Setup(r => r.AsQueryable(
                It.IsAny<Expression<Func<Product, bool>>>(),
                It.IsAny<Expression<Func<Product, object>>[]>()))
                .Returns((Expression<Func<Product, bool>> filter, Expression<Func<Product, object>>[] includes) =>
                {
                    IQueryable<Product> query = products;

                    if (includes != null)
                    {
                        foreach (var include in includes)
                        {
                            query = query.Include(include);
                        }
                    }

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    return query;
                });

            // Act
            var queriedProducts = _mockRepository.Object.AsQueryable(null,includes);

            // Assert
            Assert.Equal(2, queriedProducts.Count());
            Assert.All(queriedProducts, p => Assert.NotNull(p.Creator)); 
        }

        [Fact]
        public async Task Any_ShouldReturnTrue_WhenConditionExist()
        {
            Expression<Func<Product, bool>> filter = p => p.CreatorId == 1;
            var products = new List<Product>()
            {
                new Product()
                {
                    Id= 1,
                    Name = "test",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now,
                    CreatorId=1,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =1},
                    IsAvailable =true,
                    ManufactureEmail = "Test@gmail.com",
                    ManufacturePhone = "11111111111",
                },
                  new Product()
                {
                    Id= 2,
                    Name = "test2",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now.AddDays(1),
                    CreatorId=3,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =3},
                    IsAvailable =false,
                    ManufactureEmail = "Test1@gmail.com",
                    ManufacturePhone = "22222222222",
                },
            }
                .AsQueryable();

            _mockRepository.Setup(r => r.AnyAsync(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var queriedProducts = await _mockRepository.Object.AnyAsync(filter);

            // Assert
            Assert.True(queriedProducts);
        }

        [Fact]
        public async Task Any_ShouldReturnTrue_WhenConditionNotExist()
        {
            //Arrange
            Expression<Func<Product, bool>> filter = p => p.CreatorId == 5;
            var products = new List<Product>()
            {
                new Product()
                {
                    Id= 1,
                    Name = "test",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now,
                    CreatorId=1,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =1},
                    IsAvailable =true,
                    ManufactureEmail = "Test@gmail.com",
                    ManufacturePhone = "11111111111",
                },
                  new Product()
                {
                    Id= 2,
                    Name = "test2",
                    CreatedAt = DateTime.Now,
                    ProduceDate = DateTime.Now.AddDays(1),
                    CreatorId=3,
                    Creator = new NadinSoft.Domain.Entities.Users.User(){Id =3},
                    IsAvailable =false,
                    ManufactureEmail = "Test1@gmail.com",
                    ManufacturePhone = "22222222222",
                },
            }
                .AsQueryable();

            _mockRepository.Setup(r => r.AnyAsync(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var queriedProducts = await _mockRepository.Object.AnyAsync(filter);

            // Assert
            Assert.False(queriedProducts);
        }
    }
}
