using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface ILanceRepository : IRepository<Lance>
    {
        Task<List<Lance>> FindAllByUserIdAsync(int userId);

        Task<Lance> FindByProdIdAndUserIdAsync(int prodId, int userId);

        Task<List<Lance>> FindAllByProductIdAsync(int prodId);
    }
}
