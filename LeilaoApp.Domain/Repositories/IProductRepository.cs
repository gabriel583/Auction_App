using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> FindAllByUserIdAsync(int userId);
        Task<Product> FindByNameAsync(string name);
        Task<List<Product>> FindAllByCategoryStartWithAsync(int categoryId, string text);
        Task<List<Product>> FindAllByCategory(int categoryId);
        Task<List<Product>> FindAllByIdAsync(int prodId);
    }

}
