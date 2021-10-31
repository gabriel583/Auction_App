using Microsoft.EntityFrameworkCore;
using LeilaoApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using LeilaoApp.Domain.Repositories;
using LeilaoApp.Infrastructure.Repositories;

namespace LeilaoApp.Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        private DbContextOptions<LeilaoAppDbContext> Options { get; }

        public IProductRepository ProductRepository => new ProductRepository(new LeilaoAppDbContext(Options));

        public ICategoryRepository CategoryRepository => new CategoryRepository(new LeilaoAppDbContext(Options));

        public IUserRepository UserRepository => new UserRepository(new LeilaoAppDbContext(Options));

        public IFavoritosRepository FavoritosRepository => new FavoritosRepository(new LeilaoAppDbContext(Options));
        public ILanceRepository LanceRepository => new LanceRepository(new LeilaoAppDbContext(Options));
        public IHistoricoComprasRepository HistoricoComprasRepository => new HistoricoComprasRepository(new LeilaoAppDbContext(Options));

        public UnitOfWork(DbContextOptions<LeilaoAppDbContext> options)
        {
            Options = options;

            using (var _dbContext = new LeilaoAppDbContext(options))
            {
                _dbContext.Database.Migrate();
            }
        }

    }

}
