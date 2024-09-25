using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Objects
{
    public class ProductCreateRequest
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "UnitsInStock must be a positive number")]
        public int UnitsInStock { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than zero")]
        public decimal UnitPrice { get; set; }
    }


}
