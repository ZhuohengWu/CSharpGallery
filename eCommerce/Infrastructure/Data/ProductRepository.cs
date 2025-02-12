using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext _context) : IProductRepository
    {
        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p =>p.ProductBrand)
                .Include(p =>p.ProductType)
                .ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id) ?? new();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();

        }
    }
}
