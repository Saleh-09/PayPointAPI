using Microsoft.EntityFrameworkCore;
using PayPointAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPointAPi.DataAccess.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Snacks" },
                new Category { CategoryId = 3, CategoryName = "Household" }
            );

            modelBuilder.Entity<Store>().HasData(
                new Store { StoreId = 1, StoreName = "Main Street Supermart", Location = "Main Street, Karachi" },
                new Store { StoreId = 2, StoreName = "Green Valley Market", Location = "Sector G-11, Islamabad" },
                new Store { StoreId = 3, StoreName = "Daily Essentials", Location = "Gulberg III, Lahore" }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Orange Juice",
                    ProductDescription = "Freshly squeezed orange juice.",
                    Price = 250,
                    StockQuantity = 100,
                    ExpiryDate = new DateOnly(2025, 12, 31),
                    CategoryId = 1,
                    StoreId = 1
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Potato Chips",
                    ProductDescription = "Crispy salted potato chips.",
                    Price = 100,
                    StockQuantity = 200,
                    ExpiryDate = new DateOnly(2025, 10, 15),
                    CategoryId = 2,
                    StoreId = 2
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Dishwashing Liquid",
                    ProductDescription = "Lemon-scented dishwashing liquid.",
                    Price = 300,
                    StockQuantity = 80,
                    ExpiryDate = new DateOnly(2027, 01, 01),
                    CategoryId = 3,
                    StoreId = 3
                }
            );
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   EmployeeId = 1,
                   EmployeeName = "Ali Khan",
                   EmployeeEmail = "ali@example.com",
                   EmployeePhone = 03001234567,
                   StoreId = 1
               },
               new Employee
               {
                   EmployeeId = 2,
                   EmployeeName = "Sara Ahmed",
                   EmployeeEmail = "sara@example.com",
                   EmployeePhone = 03129876543,
                   StoreId = 2
               },
               new Employee
               {
                   EmployeeId = 3,
                   EmployeeName = "Bilal Sheikh",
                   EmployeeEmail = "bilal@example.com",
                   EmployeePhone = 03331234567,
                   StoreId = 3
               }
           );
        }
    }
}
