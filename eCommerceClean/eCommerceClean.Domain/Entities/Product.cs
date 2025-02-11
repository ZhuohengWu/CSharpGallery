﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public ProductType ProductType { get; set; } = new();
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } = new();
        public int ProductBrandId { get; set; }
    }
}
