using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductServiceClient
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:61625/api/Products")};
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Product[]> GetProductsAsync()
        {
            Product[] products=null;
            var response = await _httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<Product[]>();
            }
            return products;
        }
        
        public async Task<Product> GetProductAsync(int id)
        {
            Product product=null;
            var response = await _httpClient.GetAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }
        
        public async Task<Product> DeleteProductAsync(int id)
        {
            Product product = null;
            var response = await _httpClient.DeleteAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }
        
        public async Task<string> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("products",product);
            if (response.IsSuccessStatusCode)
            {
                return response.Headers.Location.OriginalString;
            }
            return "";
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync("products",product);
            Product updatedProduct = null;
            if (response.IsSuccessStatusCode)
            {
                updatedProduct = await response.Content.ReadAsAsync<Product>();
            }
            return updatedProduct;
        }
    }
}