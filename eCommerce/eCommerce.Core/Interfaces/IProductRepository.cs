﻿using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetAsync(int id);
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
    Task<IReadOnlyList<ProductType>> GetTypesAsync();
}
