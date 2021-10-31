using LeilaoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoApp.UWP.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public UserViewModel()
        {
            User = new User();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User User { get; set; }

        private User _loggedUser;

        public User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogged));
                OnPropertyChanged(nameof(IsNotLogged));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsLogged
        {
            get => LoggedUser != null;
        }

        public bool IsNotLogged
        {
            get => !IsLogged;
        }

        public bool IsAdmin => LoggedUser != null && LoggedUser.IsAdmin;

        internal async Task<bool> DoLoginAsync()
        {
            var user = await App.UnitOfWork.UserRepository.FindByUsernameAndPassword(User.Username, User.Password);

            LoggedUser = user;
            ShowError = user == null;
            return !ShowError;
        }

        public void DoLogout()
        {
            LoggedUser = null;
        }

        internal async Task<bool> RegisterAsync()
        {
            bool err = true;

            var user = await App.UnitOfWork.UserRepository.FindByUsername(User.Username);
            if (user == null)
            {
                user = await App.UnitOfWork.UserRepository.CreateAsync(User);
                LoggedUser = user;
                err = false;
            }

            ShowError = err;

            return !ShowError;
        }

        private bool _showError;

        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                OnPropertyChanged();
            }
        }

    }

}
