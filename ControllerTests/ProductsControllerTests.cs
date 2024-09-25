using Microsoft.AspNetCore.Mvc.Testing;
using ProjectManagementAPI;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace ControllerTests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task CreateProduct_Returns_Correct_Json_Property_Name()
        {
            var newProduct = new Dictionary<string, object>
            {
                { "product-name", "Soap" },
                { "category-id", 1 },
                { "units-in-stock", 100 },
                { "unit-price", 0.99 }
            };

            var content = new StringContent(JsonSerializer.Serialize(newProduct), System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/products", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonDocument.Parse(responseContent);

            Assert.True(jsonResponse.RootElement.TryGetProperty("product-name", out _), "Property 'product-name' not found.");
            Assert.False(jsonResponse.RootElement.TryGetProperty("productName", out _), "Property 'productName' should not exist.");
        }

        [Fact]
        public async Task CreateProduct_Returns_Created()
        {
            var newProduct = new Dictionary<string, object>
            {
                { "product-name", "Soap" },
                { "category-id", 1 },
                { "units-in-stock", 100 },
                { "unit-price", 0.99 }
            };
            var content = new StringContent(JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/products", content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_Returns_BadRequest_When_Invalid()
        {
            var invalidProduct = new Dictionary<string, object>
            {
                { "product-name", "" },
                { "category-id", 1 },
                { "units-in-stock", 100 },
                { "unit-price", 0.99 }
            };
            var content = new StringContent(JsonSerializer.Serialize(invalidProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/products", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_Returns_Ok_When_Exists()
        {
            var response = await _client.GetAsync("/api/products/3");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_Returns_NotFound_When_DoesNotExist()
        {
            var response = await _client.GetAsync("/api/products/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAllProducts_Returns_Ok()
        {
            var response = await _client.GetAsync("/api/products");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Returns_Ok_When_Exists()
        {
            var updatedProduct = new Dictionary<string, object>
            {
                { "product-name", "Updated Soap" },
                { "category-id", 1 },
                { "units-in-stock", 150 },
                { "unit-price", 1.20 }
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/products/3", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Returns_NotFound_When_DoesNotExist()
        {
            var updatedProduct = new Dictionary<string, object>
            {
                { "product-name", "Non-existent Product" },
                { "category-id", 1 },
                { "units-in-stock", 150 },
                { "unit-price", 1.20 }
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/products/9999", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_Returns_NoContent_When_Exists()
        {
            var response = await _client.DeleteAsync("/api/products/3");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_Returns_NotFound_When_DoesNotExist()
        {
            var response = await _client.DeleteAsync("/api/products/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetCategoryWithProducts_Returns_Ok_When_Exists()
        {
            var response = await _client.GetAsync("/api/categories/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
