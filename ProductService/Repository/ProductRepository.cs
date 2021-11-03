using System.Collections.Generic;
using ProductServiceCrud.Models;

namespace ProductServiceCrud.Repository
{
    public static class ProductRepository
    {
        public static List<Product> Products { get; set; }
        
        static ProductRepository()
        {
            Products = new List<Product>();
            Products.AddRange(new []
            {
                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
            });
        }
    }
    
   
}