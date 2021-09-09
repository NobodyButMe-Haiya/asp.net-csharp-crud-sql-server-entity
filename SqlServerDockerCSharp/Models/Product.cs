using System;
using System.Collections.Generic;

#nullable disable

namespace SqlServerDockerCSharp.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
