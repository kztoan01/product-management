using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Objects
{
using System.Text.Json.Serialization;

public class ProductQuery
{
    [JsonPropertyName("product-name")]
    public string ProductName { get; set; }

    [JsonPropertyName("category-id")]
    public int? CategoryId { get; set; }

    [JsonPropertyName("min-price")]
    public decimal? MinPrice { get; set; }

    [JsonPropertyName("max-price")]
    public decimal? MaxPrice { get; set; }

    [JsonPropertyName("sort-by")]
    public string SortBy { get; set; }

    [JsonPropertyName("sort-order")]
    public string SortOrder { get; set; } = "asc";

    [JsonPropertyName("page")]
    public int Page { get; set; } = 1;

    [JsonPropertyName("page-size")]
    public int PageSize { get; set; } = 10;

    [JsonPropertyName("select-fields")]
    public string[] SelectFields { get; set; }
}


}
