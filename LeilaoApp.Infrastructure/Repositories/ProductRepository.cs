using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(LeilaoAppDbContext dbContext) : base(dbContext)
        {
        }


        public override async Task<Product> UpsertAsync(Product e)
        {
            Product p = null;
            Product existing = await FindByNameAsync(e.Name);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    p = await CreateAsync(e);
                }
                else
                {
                    p = await UpdateAsync(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                p = await UpdateAsync(e);
            }

            await _dbContext.SaveChangesAsync();
            return p;
        }
        public async Task<List<Product>> FindAllByUserIdAsync(int userId)
        {
            return await _dbContext.Products.Where(c => c.UserID == userId)
                .OrderBy(c => c.Name).ToListAsync();
        }
        public async override Task<Product> FindByIdAsync(int prodId)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(c => c.Id == prodId);
        }
        public async Task<List<Product>> FindAllByIdAsync(int prodId)
        {
            return await _dbContext.Products.Where(c => c.Id == prodId)
                .OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<List<Product>> FindAllByCategoryStartWithAsync(int categoryId, string text)
        {
            return await _dbContext.Products.Where(c => c.Name.StartsWith(text)
                && (categoryId == 0 || c.CategoryId == categoryId))
                .OrderBy(c => c.Name).ToListAsync();
        }
        public async Task<List<Product>> FindAllByCategory(int categoryId)
        {
            return await _dbContext.Products.Where(c => c.CategoryId == categoryId)
                .OrderBy(c => c.Name).ToListAsync();
        }

        public override async Task<List<Product>> FindAllAsync()
        {
            return await _dbContext.Products
                .Include(m => m.Category)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(p => p.Name == name);
        }

        public override async Task<Product> FindOrCreate(Product e)
        {
            var p = await _dbContext.Products.SingleOrDefaultAsync(i => i.Name == e.Name);
            if (p == null)
            {
                p = await CreateAsync(e);
                await _dbContext.SaveChangesAsync();
            }
            return p;

        }

    }

}
