using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);
        Task<List<Category>> FindAllByNameStartWithAsync(string categoryName);
    }

}
