using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class Product : Entity
    {

        public string Name { get; set; }
        public byte[] Thumb { get; set; }
        public string Desc { get; set; }
        public double Valor { get; set; }
        public int CategoryId { get; set; }
        public int UserID { get; set; }
        public DateTime FimLeilao { get; set; }
        public Category Category { get; set; }
        public List<HistoricoCompras> HistoricoCompras { get; set; }
        public User User { get; set; }
        public List<Favoritos> Favoritos { get; set; }
        public List<Lance> Lances { get; set; }

        public Product()
        {
        }

        public Product(string name, int categoryId, string descricao, double valor, int user,DateTime Fim)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.Desc = descricao;
            this.Valor = valor;
            this.UserID = user;
            this.FimLeilao = Fim;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
