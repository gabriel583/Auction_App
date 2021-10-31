using LeilaoApp.Domain.Models;
using LeilaoApp.UWP.ViewModels;
using LeilaoApp.UWP.Views.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace LeilaoApp.UWP.Views.Categories
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Categoria_Usuario : Page
    {

        public CategoryViewModel CategoryViewModel { get; set; }

        public Categoria_Usuario()
        {
            this.InitializeComponent();
            CategoryViewModel = new CategoryViewModel();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.UserViewModel.IsLogged)
            {
                CategoryViewModel.LoadAllAsync();
                base.OnNavigatedTo(e);
            }
            else
            {
                Frame.Content = null;
            }

        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            //Frame.Navigate(typeof(GradeDeProdutos));
            if (sender is FrameworkElement fe && fe.DataContext is Category m)
            {
                CategoryViewModel.Category = m;
                Frame.Navigate(typeof(GradeDeProdutos), CategoryViewModel);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryFormPage));
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Define Content Dialog
            ContentDialog deleteCategoryDialog = new ContentDialog
            {
                Title = "Eliminar categoria permanentemente?",
                Content = "Tem a certeza que quer eliminar?",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            ContentDialogResult result = await deleteCategoryDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Category m)
                {
                    if (m.Products != null && m.Products.Any())
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        CategoryViewModel.DeleteAsync(m);
                    }
                }
            }

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Category m)
            {
                CategoryViewModel.Category = m;
                Frame.Navigate(typeof(CategoryFormPage), CategoryViewModel);
            }
        }


    }
}
