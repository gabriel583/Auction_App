using LeilaoApp.Domain.Models;
using LeilaoApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
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
using Windows.UI.Popups;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace LeilaoApp.UWP.Views.Products
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class DetalheProduto : Page
    {

        public ProductViewModel ProductViewModel { get; set; }
        public Product Product { get; set; }

        public DetalheProduto()
        {
            this.InitializeComponent();
            ProductViewModel = new ProductViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ProductViewModel = e.Parameter as ProductViewModel;
                base.OnNavigatedTo(e);

            }
        }

        private async void FavoritoAdd_Click(object sender, RoutedEventArgs e)
        {
            await ProductViewModel.AddProductToFavoritosAsync();
        }
        private void FavoritoDel_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel.DeleteFavoritosProductAsync();
        }
        private async void Lance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(bool)await ProductViewModel.AddLanceAsync(Convert.ToDouble(lance.Text)))
                {
                    var dialog = new MessageDialog("Valor Insuficiente ou Produto Indisponivel");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Lance Efetuado com sucesso");
                    await dialog.ShowAsync();
                }
            }
            catch
            {
                var dialog = new MessageDialog("Valor Não Aceitado");
                await dialog.ShowAsync();
            }

        }
    }
}
