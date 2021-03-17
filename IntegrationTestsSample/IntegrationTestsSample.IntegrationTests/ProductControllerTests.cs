using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IntegrationTestsSample.Models;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace IntegrationTestsSample.IntegrationTests
{
    public class ProductControllerTests : IClassFixture<ProductApplicationFactory>
    {
        private const string ApiUrl = "/api/products";

        private readonly HttpClient _client;

        public ProductControllerTests(ProductApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_ReturnListOfProducts()
        {
            //Arrange
            //Act
            var response = await _client.GetAsync(ApiUrl);
            var responseAsString = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<Product>>(responseAsString);

            //Assert
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.NotNull(products);
        }

        [Fact]
        public async Task GetByCode_Should_ReturnNotFound_If_ProductNotExists()
        {
            //Arrange
            var notExistingProductCode = "0000";

            //Act
            var response = await _client.GetAsync($"{ApiUrl}/{notExistingProductCode}");

            //Assert
            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [Fact]
        public async Task Get_Should_ReturnProduct_If_ProductExist()
        {
            //Arrange
            var existingProductCode = "P001";

            //Act
            var response = await _client.GetAsync($"{ApiUrl}/{existingProductCode}");
            var responseAsString = await response.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<Product>(responseAsString);

            //Act
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.NotNull(product);
        }
    }
}
