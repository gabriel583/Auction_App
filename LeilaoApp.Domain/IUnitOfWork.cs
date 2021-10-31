using LeilaoApp.Domain.Repositories;
using System;

namespace LeilaoApp.Domain
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFavoritosRepository FavoritosRepository { get; }
        IUserRepository UserRepository { get; }
        ILanceRepository LanceRepository { get; }
        IHistoricoComprasRepository HistoricoComprasRepository { get; }
    }
}
