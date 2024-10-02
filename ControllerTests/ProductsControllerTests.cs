using Microsoft.AspNetCore.Mvc.Testing;
using ProjectManagementAPI;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using BusinessObjects.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(newProduct), System.Text.Encoding.UTF8, "application/json");

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
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

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
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(invalidProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/products", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_Returns_Ok_When_Exists()
        {
            var response = await _client.GetAsync("/api/products/12");
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
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(updatedProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/products/5", content);
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
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(updatedProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/products/9999", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task DeleteProduct_Returns_DeletedProduct_When_Exists()
        {
            var response = await _client.DeleteAsync("/api/products/3");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            var deletedProduct = JsonConvert.DeserializeObject<ProductResponse>(responseContent);
            Assert.NotNull(deletedProduct);
        }
        [Fact]
        public async Task DeleteProduct_Returns_JsonInKebabCase_When_Exists()
        {
            var response = await _client.DeleteAsync("/api/products/18");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonResponse = JObject.Parse(responseContent);

            Assert.True(jsonResponse.ContainsKey("product-id"), "Expected 'product-id' key.");
            Assert.True(jsonResponse.ContainsKey("product-name"), "Expected 'product-name' key.");
            Assert.True(jsonResponse.ContainsKey("category-id"), "Expected 'category-id' key.");
            Assert.True(jsonResponse.ContainsKey("units-in-stock"), "Expected 'units-in-stock' key.");
            Assert.True(jsonResponse.ContainsKey("unit-price"), "Expected 'unit-price' key.");

            var category = jsonResponse["category"];
            Assert.NotNull(category);
            Assert.True(category.Value<JObject>().ContainsKey("category-id"), "Expected 'category-id' key inside category.");
            Assert.True(category.Value<JObject>().ContainsKey("category-name"), "Expected 'category-name' key inside category.");
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
