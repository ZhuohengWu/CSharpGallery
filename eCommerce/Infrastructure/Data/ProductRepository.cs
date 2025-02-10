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
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products.FindAsync(id) ?? new();
        }
    }
}
