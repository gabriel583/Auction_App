using LeilaoApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoApp.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Category()
        {

        }

        public Category(string categoryName)
        {
            this.Name = categoryName;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
