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
    public class FavoritosRepository : Repository<Favoritos>, IFavoritosRepository
    {
        public FavoritosRepository(LeilaoAppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Favoritos>> FindAllByUserIdAsync(int userId)
        {
            return await _dbContext.Favoritos.Where(c => c.UserId == userId)
                .OrderBy(c => c.Product.Name).ToListAsync();
        }

        public Task<Favoritos> FindByProdIdAndUserIdAsync(int prodId, int userId)
        {
            return _dbContext.Favoritos.SingleOrDefaultAsync(c => c.UserId == userId && c.ProductId == prodId);
        }

        public override Task<Favoritos> FindOrCreate(Favoritos e)
        {
            throw new NotImplementedException();
        }
        public override async Task<Favoritos> FindByIdAsync(int id)
        {
            return await _dbContext.Favoritos.SingleOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task<List<Favoritos>> FindAllByProductIdAsync(int prodId)
        {
            return await _dbContext.Favoritos.Where(c => c.ProductId == prodId)
                .OrderBy(c => c.Product.Name).ToListAsync();
        }

        public override async Task<Favoritos> UpsertAsync(Favoritos e)
        {
            Favoritos p = null;
            Favoritos existing = await FindByProdIdAndUserIdAsync(e.ProductId, e.UserId);
       
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
    }
}
