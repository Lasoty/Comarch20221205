using Microsoft.EntityFrameworkCore;
using Restauracja.Services.ProductApi.Model;

namespace Restauracja.Services.ProductApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
            base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product { 
                 Id= 1,
                 Name = "Samosa",
                 Description= "Description",
                 CategoryName = "Appetizer",
                 ImageUrl = ""
                });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Paneer Tikka",
                Description = "Description",
                CategoryName = "Appetizer",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Sweet Pie",
                Description = "Description",
                CategoryName = "Dessert",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Bread",
                Description = "Description",
                CategoryName = "Entree",
                ImageUrl = ""
            });
        }
    }
}
