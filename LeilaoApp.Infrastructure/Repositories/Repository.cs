using LeilaoApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly LeilaoAppDbContext _dbContext;

        public Repository(LeilaoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T e)
        {
            T entity = _dbContext.Set<T>().Add(e).Entity;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T e)
        {
            T entity = _dbContext.Set<T>().Remove(e).Entity;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T e)
        {
            T entity = _dbContext.Set<T>().Update(e).Entity;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public abstract Task<T> FindOrCreate(T e);

        public abstract Task<T> UpsertAsync(T e);
    }

}
