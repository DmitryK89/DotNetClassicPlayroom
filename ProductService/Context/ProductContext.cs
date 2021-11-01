using ProductService.Models;
using System.Data.Entity;

namespace ProductService.Context
{
    public class ProductContext:DbContext
    {
        public ProductContext() : base("name=ProductService")
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}