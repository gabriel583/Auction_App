using LeilaoApp.UWP.ViewModels;
using LeilaoApp.UWP.Views.Users;
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

namespace LeilaoApp.UWP.Views.Home
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public UserViewModel UserViewModel { get; set; }
        public HomePage()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
        }

        private async void Registrar_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new RegisterDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    Frame.Navigate(typeof(HomePage));
                }
            }
        }

        private async void Logar_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    Frame.Navigate(typeof(HomePage));
                }
            }
        }
    }
}
