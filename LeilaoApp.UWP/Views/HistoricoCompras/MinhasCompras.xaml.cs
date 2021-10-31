﻿using LeilaoApp.Domain.Models;
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

namespace LeilaoApp.UWP.Views.HistoricoCompras
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MinhasCompras : Page
    {

        public ProductViewModel ProductViewModel { get; set; }

        public MinhasCompras()
        {
            this.InitializeComponent();
            ProductViewModel = new ProductViewModel();
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Product m)
            {
                ProductViewModel.Product = m;
                Frame.Navigate(typeof(DetalheProduto), ProductViewModel);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProductViewModel.AddHistoricoComprasAsync();
            ProductViewModel.LoadAllHistoricoComprasByLoggedUserAsync();
            base.OnNavigatedTo(e);
        }
    }
}
