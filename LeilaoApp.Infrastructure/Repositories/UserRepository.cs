using LeilaoApp.Domain.Models;
using LeilaoApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LeilaoAppDbContext dbContext) : base(dbContext) { }

        public async Task<User> FindByUsername(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(e => e.Username == username);
        }

        public async Task<User> FindByUsernameAndPassword(string username, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(e => e.Username == username
                    && e.Password == password);
        }

        public override async Task<User> FindOrCreate(User e)
        {
            var c = await _dbContext.Users.SingleOrDefaultAsync(i => i.Username == e.Username);
            if (c == null)
            {
                c = await CreateAsync(e);
                await _dbContext.SaveChangesAsync();
            }
            return c;
        }

        public override async Task<User> UpsertAsync(User e)
        {
            User f = null;
            User existing = await FindByUsername(e.Username);

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
