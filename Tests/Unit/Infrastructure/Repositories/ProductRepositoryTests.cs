using Core.Models;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Unit.Infrastructure.Repositories
{
    public class ProductRepositoryTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            return context;
        }

        [Fact]
        public async Task AddAsync_ShouldAddProductToDatabase()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            var product = new Product
            {
                Code = "TEST001",
                Name = "Test Product",
                Price = 10.00m,
                StockQuantity = 100
            };

            // Act
            var result = await repository.AddAsync(product);

            // Assert
            Assert.NotEqual(0, result.Id);
            Assert.Equal("TEST001", result.Code);
            var savedProduct = await context.Products.FindAsync(result.Id);
            Assert.NotNull(savedProduct);
            Assert.Equal("Test Product", savedProduct.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            var product = new Product
            {
                Code = "TEST001",
                Name = "Test Product",
                Price = 10.00m,
                StockQuantity = 100
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            var product = new Product
            {
                Code = "TEST001",
                Name = "Test Product",
                Price = 10.00m,
                StockQuantity = 100
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            product.Name = "Updated Product";
            await repository.UpdateAsync(product);

            // Assert
            var updatedProduct = await context.Products.FindAsync(product.Id);
            Assert.Equal("Updated Product", updatedProduct.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct_WhenProductExists()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            var product = new Product
            {
                Code = "TEST001",
                Name = "Test Product",
                Price = 10.00m,
                StockQuantity = 100
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            await repository.DeleteAsync(product.Id);

            // Assert
            var deletedProduct = await context.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingProducts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            await context.Products.AddRangeAsync(
                new Product { Code = "TEST001", Name = "Test Product 1", Price = 10.00m },
                new Product { Code = "TEST002", Name = "Test Product 2", Price = 20.00m },
                new Product { Code = "OTHER001", Name = "Other Product", Price = 30.00m }
            );
            await context.SaveChangesAsync();

            // Act
            var results = await repository.SearchAsync("Test");

            // Assert
            Assert.Equal(2, results.Count());
            Assert.All(results, p => Assert.Contains("Test", p.Name));
        }
    }
}
