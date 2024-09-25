﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Objects
{
    public class ProductQuery
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; } 
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

}
