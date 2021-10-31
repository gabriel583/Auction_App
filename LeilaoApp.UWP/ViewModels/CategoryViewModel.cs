using LeilaoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LeilaoApp.UWP.ViewModels
{
    public class CategoryViewModel : BindableBase
    {
        public ObservableCollection<Category> Categories { get; set; }

        private Category _category;

        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                CategoryName = _category?.Name;
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                Set(ref _categoryName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        public CategoryViewModel()
        {
            Category = new Category();
            Categories = new ObservableCollection<Category>();
        }

        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(CategoryName);
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public async void LoadAllAsync()
        {
            var list = await App.UnitOfWork.CategoryRepository
                .FindAllAsync();
            Categories.Clear();
            foreach (var l in list)
            {
                Categories.Add(l);
            }
        }

        internal async void DeleteAsync(Category p)
        {
            await App.UnitOfWork.CategoryRepository.DeleteAsync(p);
            Categories.Remove(p);
        }

        internal async Task<Category> UpsertAsync()
        {
            Category.Name = CategoryName;
            return await App.UnitOfWork.CategoryRepository
                .UpsertAsync(Category);
        }

        internal async Task<ObservableCollection<Category>>
            LoadCategoriesByNameStartWithAsync(string categoryName)
        {
            var list = await App.UnitOfWork.CategoryRepository
                .FindAllByNameStartWithAsync(categoryName);
            return new ObservableCollection<Category>(list);
        }
    }

}
