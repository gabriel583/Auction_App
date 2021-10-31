using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class Lance : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Valor { get; set; }
        public DateTime HoraLance { get; set; }
        public User User { get; set; }

        public Product Product { get; set; }

        public Lance()
        {
        }
        public Lance(int userid, int productid, double valor)
        {
            UserId = userid;
            ProductId = productid;
            Valor = valor;
            HoraLance = System.DateTime.Now;
        }
    }
}
