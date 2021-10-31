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
    public class HistoricoComprasRepository : Repository<HistoricoCompras>, IHistoricoComprasRepository
    {
        public HistoricoComprasRepository(LeilaoAppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<HistoricoCompras>> FindAllByUserIdAsync(int userId)
        {
            return await _dbContext.HistoricoCompras.Where(c => c.UserId == userId)
                .OrderBy(c => c.Product.Name).ToListAsync();
        }

        public Task<HistoricoCompras> FindByProdIdAsync(int prodId)
        {
            return _dbContext.HistoricoCompras.SingleOrDefaultAsync(c => c.ProductId == prodId);
        }

        public override Task<HistoricoCompras> FindOrCreate(HistoricoCompras e)
        {
            throw new NotImplementedException();
        }

        public override async Task<HistoricoCompras> UpsertAsync(HistoricoCompras e)
        {
            HistoricoCompras l = null;
            HistoricoCompras existing = await FindByProdIdAsync(e.ProductId);

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
