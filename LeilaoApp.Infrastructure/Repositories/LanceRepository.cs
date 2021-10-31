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
    public class LanceRepository : Repository<Lance>, ILanceRepository
    {

        public LanceRepository(LeilaoAppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Lance>> FindAllByProductIdAsync(int prodId)
        {
            return await _dbContext.Lance.Where(c => c.ProductId == prodId)
                .OrderBy(c => c.Product.Name).ToListAsync();
        }

        public async Task<List<Lance>> FindAllByUserIdAsync(int userId)
        {
            return await _dbContext.Lance.Where(c => c.UserId == userId)
                .OrderBy(c => c.Product.Name).ToListAsync();
        }

        public Task<Lance> FindByProdIdAndUserIdAsync(int prodId, int userId)
        {
            return _dbContext.Lance.SingleOrDefaultAsync(c => c.UserId == userId && c.ProductId == prodId);
        }

        public override Task<Lance> FindOrCreate(Lance e)
        {
            throw new NotImplementedException();
        }

        public override async Task<Lance> UpsertAsync(Lance e)
        {
            Lance l = null;
            Lance existing = await FindByProdIdAndUserIdAsync(e.ProductId, e.UserId);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    l = await CreateAsync(e);
                }
                else
                {
                    l = await UpdateAsync(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                l = await UpdateAsync(e);
            }

            await _dbContext.SaveChangesAsync();
            return l;
        }
    }
}
