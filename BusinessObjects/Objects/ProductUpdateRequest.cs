using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Objects
{
    public class ProductUpdateRequest
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }  
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
