using Microsoft.EntityFrameworkCore;
using SwiftShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShop.Application.Commons.Data
{
    public interface ISwiftShopDbContext
    {
        DbSet<Product> Products { get; }
    }
}
