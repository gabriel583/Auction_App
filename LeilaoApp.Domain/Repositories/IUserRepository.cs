using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsername(string username);

        Task<User> FindByUsernameAndPassword(string username, string password);
    }

}
