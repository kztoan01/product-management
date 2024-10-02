using BusinessObjects;
using BusinessObjects.Objects;
using Microsoft.AspNetCore.Mvc;
using Services.Product_Service;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery(Name = "product-name")] string? productName,
        [FromQuery(Name = "category-id")] int? categoryId,
        [FromQuery(Name = "min-price")] decimal? minPrice,
        [FromQuery(Name = "max-price")] decimal? maxPrice,
        [FromQuery(Name = "sort-by")] string? sortBy,
        [FromQuery(Name = "sort-order")] string? sortOrder = "asc",
        [FromQuery(Name = "page")] int page = 1,
        [FromQuery(Name = "page-size")] int pageSize = 99,
        [FromQuery(Name = "select-fields")] string[] selectFields = null)
        {
            var query = new ProductQuery
            {
                ProductName = productName,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize,
                SelectFields = selectFields
            };

            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();  

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                UnitsInStock = request.UnitsInStock,
                UnitPrice = request.UnitPrice
            };

            var createdProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }

            product.ProductName = request.ProductName;
            product.CategoryId = request.CategoryId;
            product.UnitsInStock = request.UnitsInStock;
            product.UnitPrice = request.UnitPrice;

            var updatedProduct = await _productService.UpdateProductAsync(product);
            return Ok(updatedProduct); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _productService.DeleteProductAsync(id);
            if (deletedProduct == null)
            {
                return NotFound(ModelState);
            }
            return Ok(deletedProduct); 
        }
        /*[HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery(Name = "product-name")] string? productName,
        [FromQuery(Name = "category-id")] int? categoryId,
        [FromQuery(Name = "min-price")] decimal? minPrice,
        [FromQuery(Name = "max-price")] decimal? maxPrice,
        [FromQuery(Name = "sort-by")] string? sortBy,
        [FromQuery(Name = "sort-order")] string? sortOrder = "asc",
        [FromQuery(Name = "page")] int page = 1,
        [FromQuery(Name = "page-size")] int pageSize = 99,
        [FromQuery(Name = "select-fields")] string[] selectFields = null)
        {
            var query = new ProductQuery
            {
                ProductName = productName,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize,
                SelectFields = selectFields
            };

            var products = await _productService.SearchProductsAsync(query);
            return Ok(products); 
        }*/

    }

}
