using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public List<Product> Products { get; set; }
        public List<Lance> Lances { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Favoritos> Favoritos { get; set; }
        public List<HistoricoCompras> HistoricoCompras { get; set; }
        public User() { }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);
                password = BitConverter.ToString(hashData).Replace("-", "").ToUpper();
            }

        }

        public bool IsAdmin { get; set; }
    }

}
