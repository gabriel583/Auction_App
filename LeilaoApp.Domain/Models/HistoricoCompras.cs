using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class HistoricoCompras : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Valor { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public HistoricoCompras()
        {
        }
        public HistoricoCompras(int userid, int productid, double valor)
        {
            UserId = userid;
            ProductId = productid;
            Valor = valor;
        }
    }
}
