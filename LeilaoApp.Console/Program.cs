using LeilaoApp.Domain;
using LeilaoApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LeilaoAppConsole
{
    class Program
    {
        public static string SqlConnectionString = @"Server=localhost; User Id=usuario; Password=020292; Initial Catalog=Leilao; Connect Timeout = 30";

        public static IUnitOfWork UOW { get; private set; }

        static void Main(string[] args)
        {
            ConfigReposAsync();
        }
        static void ConfigReposAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeilaoAppDbContext>();
            optionsBuilder.UseSqlServer(Program.SqlConnectionString);

            UOW = new UnitOfWork(optionsBuilder.Options);

        }
    }
}
