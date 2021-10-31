using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface IHistoricoComprasRepository : IRepository<HistoricoCompras>
    {
        Task<List<HistoricoCompras>> FindAllByUserIdAsync(int userId);

        Task<HistoricoCompras> FindByProdIdAsync(int prodId);

    }
}
