using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class Favoritos: Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }

        public Product Product { get; set; }

        public Favoritos()
        {
        }
        public Favoritos(int userid, int productid)
        {
            UserId = userid;
            ProductId = productid;
        }
    }
}
