using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductServiceClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            var client = new ProductClient();

            Console.WriteLine("Step #1 Initial");
            var initialProducts = await client.GetProductsAsync();
            ShowProducts(initialProducts);
            Console.WriteLine("");
            
            Console.WriteLine("Step #2 Create new product");
            Product newProduct = new Product
            {
                Name = "Gizmo",
                Price = 100,
                Category = "Widgets",
                Id = "10"
            };
            var url = await client.CreateProductAsync(newProduct);
            Console.WriteLine($"Created at {url}");
            Console.WriteLine("");
            
            Console.WriteLine("Step #3 Read product");
            var productReadById = await client.GetProductAsync(10);
            ShowProduct(productReadById);
            Console.WriteLine("");
            
            Console.WriteLine("Step #4 UpdateProduct");
            newProduct.Price=120;
            await client.UpdateProductAsync(newProduct);
            var afterUpdateProducts = await client.GetProductsAsync();
            ShowProducts(afterUpdateProducts);
            Console.WriteLine("");
            
            Console.WriteLine("Step #5 Delete Product");
            await client.DeleteProductAsync(10);
            var afterDeleteProducts = await client.GetProductsAsync();
            ShowProducts(afterDeleteProducts);
            Console.WriteLine("");
        }
        
        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}, " +
                              $"Price: {product.Price}, " +
                              $"Category: {product.Category}, " +
                              $"Id: {product.Id}");
        }
        
        static void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                ShowProduct(product);
            }
        }
    }
}