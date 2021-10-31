using LeilaoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeilaoApp.Infrastructure
{
    public class LeilaoAppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }
        public DbSet<Lance> Lance { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoricoCompras> HistoricoCompras { get; set; }

        public LeilaoAppDbContext()
        {
        }

        public LeilaoAppDbContext(DbContextOptions<LeilaoAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<Product>().Property(i => i.Name)
                .IsRequired().HasMaxLength(256);
            modelBuilder.Entity<Product>().Property(i => i.Valor)
                .IsRequired().HasMaxLength(10);

            //Produtos
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(t => t.User)
                .WithMany(t => t.Products)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().Property(i => i.FimLeilao).IsRequired();

            //Favoritos
            modelBuilder.Entity<Favoritos>()
                .HasIndex(c => new { c.ProductId, c.UserId }).IsUnique();
            modelBuilder.Entity<Favoritos>()
                .HasOne(t => t.User)
                .WithMany(t => t.Favoritos)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favoritos>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Lance
            modelBuilder.Entity<Lance>()
                .HasKey(c => new { c.Id, c.ProductId, c.UserId });
            modelBuilder.Entity<Lance>()
                .HasOne(t => t.User)
                .WithMany(t => t.Lances)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lance>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Lances)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lance>().Property(i => i.Valor)
                .IsRequired().HasMaxLength(10);

            //HistoricoCompras
            modelBuilder.Entity<HistoricoCompras>()
                .HasIndex(c => new { c.ProductId, c.UserId }).IsUnique();
            modelBuilder.Entity<HistoricoCompras>()
                .HasOne(t => t.User)
                .WithMany(t => t.HistoricoCompras)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistoricoCompras>()
                .HasOne(p => p.Product)
                .WithMany(p => p.HistoricoCompras)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistoricoCompras>().Property(i => i.Valor).IsRequired();

            //Categorias
            modelBuilder.Entity<Category>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Category>().Property(i => i.Name)
                .IsRequired().HasMaxLength(256);

            //Usuarios
            modelBuilder.Entity<User>().HasData(new User
            { Id = 1, Username = "admin", Password = "admin", IsAdmin = true, Nome = "admin" , Email = "adm@adm.com"});

        }

    }

}
