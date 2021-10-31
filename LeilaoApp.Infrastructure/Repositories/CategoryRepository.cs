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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(LeilaoAppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Category>> FindAllByNameStartWithAsync(string categoryName)
        {
            return _dbContext.Categories.Where(c => c.Name.StartsWith(categoryName))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public Task<Category> FindByNameAsync(string name)
        {
            return _dbContext.Categories.SingleOrDefaultAsync(c => c.Name == name);
        }

        public override async Task<Category> FindOrCreate(Category e)
        {
            var c = await _dbContext.Categories.SingleOrDefaultAsync(i => i.Name == e.Name);
            if (c == null)
            {
                c = await CreateAsync(e);
                await _dbContext.SaveChangesAsync();
            }
            return c;
        }

        public override async Task<Category> UpsertAsync(Category e)
        {
            Category f = null;
            Category existing = await FindByNameAsync(e.Name);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    f = await CreateAsync(e);
                }
                else
                {
                    f = await UpdateAsync(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                f = await UpdateAsync(e);
            }
            await _dbContext.SaveChangesAsync();

            return f;
        }
    }

}
