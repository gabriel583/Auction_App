using LeilaoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.UWP.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        public ObservableCollection<Product> Products { get; set; }

        public Favoritos Favoritos { get; set; }

        public Lance Lance { get; set; }

        public double tempo;

        private DateTime _fimLeilao;
        public DateTime Fimleilao
        {
            get { return _fimLeilao; }
            set
            {
                Set(ref _fimLeilao, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }
        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                Set(ref _categoryName, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                Set(ref _productName, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set
            {
                Set(ref _descricao, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }
        private double _valor;
        public double Valor
        {
            get { return _valor; }
            set
            {
                Set(ref _valor, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                Set(ref _userId, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        private byte[] _thumb;

        public byte[] Thumb
        {
            get { return _thumb; }
            set
            {
                Set(ref _thumb, value);
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrEmpty(ProductName) && !string.IsNullOrEmpty(CategoryName) && Valor != 0 && !string.IsNullOrEmpty(Descricao)
                    && tempo > 0 && Valor > 0 ;
                return res;
            }
        }

        public ProductViewModel()
        {
            Product = new Product();
            Products = new ObservableCollection<Product>();

            TitleText = "Products";
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

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                CategoryName = _product?.Category?.Name;
                ProductName = _product?.Name;
                Thumb = _product?.Thumb;
                Descricao = _product?.Desc;
                Valor = _product.Valor;
                Fimleilao = _product.FimLeilao;
                
            }
        }

        internal async void DeleteProductAsync(Product m)
        {

            var list = await App.UnitOfWork.FavoritosRepository.FindAllByProductIdAsync(m.Id);

            Products.Clear();
            foreach (var l in list)
            {
                await App.UnitOfWork.FavoritosRepository.DeleteAsync(l);
            }
            await App.UnitOfWork.ProductRepository.DeleteAsync(m);
            Products.Remove(m);
        }

        public async void LoadAllByLoggedUserAsync()
        {
            List<Product> list = null;
            User logged = App.UserViewModel.LoggedUser;

            var userId = logged.Id;
            list = await App.UnitOfWork.ProductRepository
                .FindAllByUserIdAsync(userId);

            Products.Clear();
            foreach (var l in list)
            {
                Products.Add(l);
            }
        }

        public async void LoadAllAsync()
        {
            var list = await App.UnitOfWork.ProductRepository.FindAllAsync();
            Products.Clear();
            foreach (var l in list)
            {
                Products.Add(l);
            }
        }

        internal async Task<Product> AddProductAsync()
        {
            // Get existing Category or Create a new one
            Category category = new Category(CategoryName);
            Category categoryUpdated = await App.UnitOfWork.CategoryRepository.FindByNameAsync(category.Name);
            User logged = App.UserViewModel.LoggedUser;
            // Fill Product fields
            Product.CategoryId = categoryUpdated.Id;
            Product.Category = null;
            Product.Name = ProductName;
            Product.Thumb = Thumb;
            Product.Desc = Descricao;
            Product.Valor = Valor;
            Product.UserID = logged.Id;
            Product.FimLeilao = DateTime.Now.AddHours(tempo);

            // Create Product
            return await App.UnitOfWork.ProductRepository.UpsertAsync(Product);
        }

        internal async Task<ObservableCollection<Category>> LoadCategoriesByNameStartWithAsync(string categoryName)
        {
            var list = await App.UnitOfWork.CategoryRepository.FindAllByNameStartWithAsync(categoryName);
            return new ObservableCollection<Category>(list);
        }

        internal async Task<ObservableCollection<Product>> LoadProductsByCategoryStartWithAsync(string text)
        {
            var list = new List<Product>();

            int categoryId = 0;
            Category cat = await App.UnitOfWork.CategoryRepository.FindByNameAsync(CategoryName);

            // Teaser expression
            categoryId = cat?.Id ?? -1;

            list = await App.UnitOfWork.ProductRepository
                .FindAllByCategoryStartWithAsync(categoryId, text);


            return new ObservableCollection<Product>(list);
        }
        public async void LoadProductsByCategory(string text)
        {
            var list = new List<Product>();
            DateTime agora = DateTime.Now;
            int categoryId = 0;
            Category cat = await App.UnitOfWork.CategoryRepository.FindByNameAsync(text);

           
            categoryId = cat.Id;

            list = await App.UnitOfWork.ProductRepository
                .FindAllByCategory(categoryId);

            Products.Clear();
            foreach (var l in list)
            {
                if(l.FimLeilao > agora)
                    Products.Add(l);
            }
        }


        internal async void DeleteAsync(Favoritos p)
        {
            await App.UnitOfWork.FavoritosRepository.DeleteAsync(p);
        }

        internal async Task<object> AddProductToFavoritosAsync()
        {

            Favoritos favoritos = new Favoritos(App.UserViewModel.LoggedUser.Id, Product.Id);

            return await App.UnitOfWork.FavoritosRepository
                .UpsertAsync(favoritos) != null;
        }

        internal async void DeleteFavoritosProductAsync()
        {
            User logged = App.UserViewModel.LoggedUser;
            Favoritos fav = await App.UnitOfWork.FavoritosRepository.FindByProdIdAndUserIdAsync(Product.Id, logged.Id);
            if( fav != null){
                await App.UnitOfWork.FavoritosRepository.DeleteAsync(fav);
            }
        }

        public async void LoadProductsById(int id)
        {
            var list = new List<Product>();

            list = await App.UnitOfWork.ProductRepository
                .FindAllByIdAsync(id);

            Products.Clear();
            foreach (var l in list)
            {
                Products.Add(l);
            }
        }
        public async void LoadAllFavoritosByLoggedUserAsync()
        {
            List<Favoritos> list = null;
            User logged = App.UserViewModel.LoggedUser;

            Product p = new Product();
            var userId = logged.Id;
            list = await App.UnitOfWork.FavoritosRepository
                .FindAllByUserIdAsync(userId);

            Products.Clear();
            foreach (var l in list)
            {
                p = await App.UnitOfWork.ProductRepository.FindByIdAsync(l.ProductId);
                //ProductViewModel.LoadProductsById(l.ProductId);
                Products.Add(p);
            }
        }
        internal async Task<object> AddLanceAsync(double valor)
        {
            User logged = App.UserViewModel.LoggedUser;
            DateTime agora = DateTime.Now;
            if (valor > Product.Valor && Product.FimLeilao > agora)
            {
                Lance lance;
                lance = new Lance(logged.Id, Product.Id, valor);
                Product.Valor = valor;
                await App.UnitOfWork.ProductRepository.UpdateAsync(Product);
                return await App.UnitOfWork.LanceRepository
                    .UpsertAsync(lance) != null;
            }
            return false;
        }
        public static async void AddHistoricoComprasAsync()
        {

            var list = await App.UnitOfWork.ProductRepository.FindAllAsync();
            foreach (var l in list)
            {
                int id = 0;
                DateTime agora = DateTime.Now;
                double valormax = 0;
                if (l.FimLeilao < agora)
                {
                    var lances = await App.UnitOfWork.LanceRepository.FindAllByProductIdAsync(l.Id);
                    foreach (var lance in lances)
                    {
                        if (lance.Valor > valormax)
                        {
                            valormax += lance.Valor;
                            id = lance.UserId;
                        }
                    }
                    if (id != 0)
                    {
                        HistoricoCompras Historico;
                        Historico = new HistoricoCompras(id, l.Id, valormax);
                        await App.UnitOfWork.HistoricoComprasRepository
                                  .UpsertAsync(Historico);
                    }
                }
            }
        }
        public async void LoadAllHistoricoComprasByLoggedUserAsync()
        {
            List<HistoricoCompras> list = null;
            User logged = App.UserViewModel.LoggedUser;

            Product p = new Product();
            var userId = logged.Id;
            list = await App.UnitOfWork.HistoricoComprasRepository
                .FindAllByUserIdAsync(userId);

            Products.Clear();
            foreach (var l in list)
            {
                p = await App.UnitOfWork.ProductRepository.FindByIdAsync(l.ProductId);
                Products.Add(p);
            }
        }
    }
}
