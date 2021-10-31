using LeilaoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.UWP.ViewModels
{
    public class FavoritosViewModel : BindableBase
    {

        private Favoritos favoritos;
        public Favoritos Favorito
        {
            get { return favoritos; }
            set
            {
                favoritos = value;
                IndiceId = (int)(favoritos?.Id);
                ProdutoId = (int)(favoritos?.ProductId);
                UsuarioId = (int)(favoritos?.UserId);
            }
        }
        public ObservableCollection<Favoritos> Favoritos { get; set; }

        public ProductViewModel ProductViewModel { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public FavoritosViewModel()
        {
            Favorito = new Favoritos();
            Favoritos = new ObservableCollection<Favoritos>();

            TitleText = "Favoritos";
        }

        private int _indiceId;
        public int IndiceId
        {
            get { return _indiceId; }
            set
            {
                Set(ref _indiceId, value);
            }
        }

        private int _produtoId;
        public int ProdutoId
        {
            get { return _produtoId; }
            set
            {
                Set(ref _produtoId, value);
            }
        }

        private int _usuarioId;
        public int UsuarioId
        {
            get { return _usuarioId; }
            set
            {
                Set(ref _usuarioId, value);
            }
        }

        private string _titleText;
        public string TitleText
        {
            get { return _titleText; }
            set
            {
                Set(ref _titleText, value);
            }
        }

        public async void LoadAllAsync()
        {
            var userId = App.UserViewModel.LoggedUser.Id;
            var list = await App.UnitOfWork.FavoritosRepository
                .FindAllByUserIdAsync(userId);

            Favoritos.Clear();
            foreach (var l in list)
            {
                Favoritos.Add(l);
            }
        }
        internal async void DeleteProductAsync(Favoritos m)
        {
            await App.UnitOfWork.FavoritosRepository.DeleteAsync(m);
            Favoritos.Remove(m);
        }
        public async void LoadAllByLoggedUserAsync()
        {
            List<Favoritos> list = null;
            User logged = App.UserViewModel.LoggedUser;

            Product p = new Product();
            var userId = logged.Id;
            list = await App.UnitOfWork.FavoritosRepository
                .FindAllByUserIdAsync(userId);

            Favoritos.Clear();
            foreach (var l in list)
            {
                p = await App.UnitOfWork.ProductRepository.FindByIdAsync(l.ProductId);
                //ProductViewModel.LoadProductsById(l.ProductId);
                Products.Add(p);
            }
        }

    }
}
