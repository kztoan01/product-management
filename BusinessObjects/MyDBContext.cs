using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public MyDbContext(DbContextOptions dbContextOptions)
       : base(dbContextOptions)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Condiments" },
                new Category { CategoryId = 3, CategoryName = "Confections" },
                new Category { CategoryId = 4, CategoryName = "Dairy Products" },
                new Category { CategoryId = 5, CategoryName = "Grains/Cereals" },
                new Category { CategoryId = 6, CategoryName = "Meat/Poultry" },
                new Category { CategoryId = 7, CategoryName = "Produce" },
                new Category { CategoryId = 8, CategoryName = "Seafood" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Apple iPhone 14", CategoryId = 3, UnitsInStock = 100, UnitPrice = 999.99M },
                new Product { ProductId = 2, ProductName = "Samsung Galaxy S22", CategoryId = 3, UnitsInStock = 150, UnitPrice = 799.99M },
                new Product { ProductId = 3, ProductName = "Sony WH-1000XM5 Headphones", CategoryId = 5, UnitsInStock = 200, UnitPrice = 299.99M },
                new Product { ProductId = 4, ProductName = "Dell XPS 13 Laptop", CategoryId = 4, UnitsInStock = 80, UnitPrice = 1500.00M },
                new Product { ProductId = 5, ProductName = "Apple MacBook Pro", CategoryId = 5, UnitsInStock = 50, UnitPrice = 2300.00M },
                new Product { ProductId = 6, ProductName = "Amazon Echo Dot", CategoryId = 7, UnitsInStock = 300, UnitPrice = 49.99M },
                new Product { ProductId = 7, ProductName = "Bose SoundLink Speaker", CategoryId = 6, UnitsInStock = 120, UnitPrice = 199.99M },
                new Product { ProductId = 8, ProductName = "Google Pixel 7", CategoryId = 2, UnitsInStock = 90, UnitPrice = 899.99M },
                new Product { ProductId = 9, ProductName = "HP Envy 15 Laptop", CategoryId = 1, UnitsInStock = 60, UnitPrice = 1399.99M },
                new Product { ProductId = 10, ProductName = "Lenovo ThinkPad X1 Carbon", CategoryId = 2, UnitsInStock = 70, UnitPrice = 1799.99M },
                new Product { ProductId = 11, ProductName = "Apple iPad Pro", CategoryId = 4, UnitsInStock = 130, UnitPrice = 1099.99M },
                new Product { ProductId = 12, ProductName = "Microsoft Surface Laptop 4", CategoryId = 3, UnitsInStock = 110, UnitPrice = 1199.99M },
                new Product { ProductId = 13, ProductName = "JBL Charge 5 Speaker", CategoryId = 6, UnitsInStock = 240, UnitPrice = 149.99M },
                new Product { ProductId = 14, ProductName = "OnePlus 9 Pro", CategoryId = 5, UnitsInStock = 90, UnitPrice = 799.99M },
                new Product { ProductId = 15, ProductName = "Asus ROG Zephyrus Gaming Laptop", CategoryId = 7, UnitsInStock = 30, UnitPrice = 2199.99M },
                new Product { ProductId = 16, ProductName = "Fitbit Versa 3", CategoryId = 1, UnitsInStock = 400, UnitPrice = 229.99M },
                new Product { ProductId = 17, ProductName = "Sony PlayStation 5", CategoryId = 8, UnitsInStock = 10, UnitPrice = 499.99M },
                new Product { ProductId = 18, ProductName = "Apple AirPods Pro", CategoryId = 7, UnitsInStock = 350, UnitPrice = 249.99M },
                new Product { ProductId = 19, ProductName = "GoPro HERO10", CategoryId = 6, UnitsInStock = 90, UnitPrice = 399.99M },
                new Product { ProductId = 20, ProductName = "Nikon D7500 Camera", CategoryId = 8, UnitsInStock = 40, UnitPrice = 1199.99M }
            );
            // modelBuilder.Entity<AppUser>()
            //     .HasNoKey();
            List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    },
                };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
