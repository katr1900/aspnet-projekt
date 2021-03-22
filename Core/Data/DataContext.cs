using System;
using System.Collections.Generic;
using Foodie.Core.Entities;
using Foodie.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Foodie.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(a => a.Id);
            modelBuilder.Entity<Category>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>().HasKey(a => a.Id);
            modelBuilder.Entity<Product>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(a => a.Price).HasPrecision(14, 2);
         
            modelBuilder.Entity<Address>().HasKey(a => a.Id);
            modelBuilder.Entity<Address>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Administrator>().HasKey(a => a.Id);
            modelBuilder.Entity<Administrator>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Address>().HasKey(a => a.Id);
            modelBuilder.Entity<Address>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>().HasKey(a => a.Id);
            modelBuilder.Entity<Order>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderLine>().HasKey(a => a.Id);
            modelBuilder.Entity<OrderLine>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Person>().HasKey(a => a.Id);
            modelBuilder.Entity<Person>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().Property(a => a.Id).ValueGeneratedOnAdd();

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Grönsaker" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Fisk" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Kött" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Snacks" });

            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Bifftomat eko", Price = 5, CategoryId = 1 });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 2, Name = "Gurka", Price = 10, CategoryId = 1 });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 3, Name = "Kruksallad", Price = 20, CategoryId = 1 });

            var userModel = new UserModel(null);
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "test", Password = userModel.HashPassword("1234"), Email = "test@test.se", Phone = "073 4444 423" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 1, Firstname = "Test", Lastname = "Testsson", UserId = 1 });
            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Street = "Sveagatan 12", Zip = "11111", City = "Solna", UserId = 1 });

            var administratorModel = new AdministratorModel(null);
            modelBuilder.Entity<Administrator>().HasData(new Administrator { Id = 1, Username = "admin", Password = administratorModel.HashPassword("1234") });
        }
    }
}
