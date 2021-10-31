using LeilaoApp.Domain.Models;
using LeilaoApp.UWP.ViewModels;
using LeilaoApp.UWP.Views;
using LeilaoApp.UWP.Views.Categories;
using LeilaoApp.UWP.Views.Favoritos;
using LeilaoApp.UWP.Views.HistoricoCompras;
using LeilaoApp.UWP.Views.Home;
using LeilaoApp.UWP.Views.Products;
using LeilaoApp.UWP.Views.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace LeilaoApp.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public UserViewModel UserViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            AppFrame.Navigate(typeof(HomePage));
            UserViewModel = App.UserViewModel;
            // Define standard insert data if necessary
            //SetupAppData();
        }
        public Frame AppFrame => frame;
  
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "categories":
                        AppFrame.Navigate(typeof(Categoria_Usuario));
                        break;
                    case "cat_adm":
                        AppFrame.Navigate(typeof(ManageCategoriesPage));
                        break;
                    case "home":
                        AppFrame.Navigate(typeof(HomePage));
                        break;
                    case "prod_adm":
                        AppFrame.Navigate(typeof(ManageProductsPage));
                        break;
                    case "products":
                        AppFrame.Navigate(typeof(ProdutosUsuarios));
                        break;
                    case "favoritos":
                        AppFrame.Navigate(typeof(FavoritosPage));
                        break;
                    case "minhascompras":
                        AppFrame.Navigate(typeof(MinhasCompras));
                        break;

                }
            }
        }
        private async void btnRegister_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dlg = new RegisterDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(HomePage));
                }
            }
        }

        private void btnLogout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserViewModel.DoLogout();
            AppFrame.BackStack.Clear();
            AppFrame.Content = null;
            AppFrame.Navigate(typeof(HomePage));
        }

        private async void btnLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(HomePage));
                }
            }
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AppFrame.CanGoBack)
            {
                AppFrame.GoBack();
            }
        }

    }
}
