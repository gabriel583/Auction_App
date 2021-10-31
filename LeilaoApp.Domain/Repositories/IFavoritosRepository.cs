using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface IFavoritosRepository : IRepository<Favoritos>
    {
        Task<List<Favoritos>> FindAllByUserIdAsync(int userId);

        Task<Favoritos> FindByProdIdAndUserIdAsync(int prodId, int userId);

        Task<List<Favoritos>> FindAllByProductIdAsync(int prodId);
    }

}
