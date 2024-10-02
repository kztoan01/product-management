using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Objects
{
    public class SearchQuery
    {
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Dictionary<string, object>? Filters { get; set; } = new Dictionary<string, object>();
    }
}