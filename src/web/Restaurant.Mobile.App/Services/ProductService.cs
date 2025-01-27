using Mobile.App.HttpClients;
using Restaurant.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Mobile.App.Services
{
    public interface IProductService
    {
        Task<ProductModel> AddProductAsync(ProductModel product);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task<ProductModel> UpdateProductAsync(ProductModel product);
    }

    public class ProductService : IProductService
    {
        private readonly IRestaurantHttpClient _restaurantHttpClient;

        public ProductService(IRestaurantHttpClient restaurantHttpClient)
        {
            _restaurantHttpClient = restaurantHttpClient;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var response = await _restaurantHttpClient.GetAsync("Product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var response = await _restaurantHttpClient.GetAsync($"Product/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductModel>();
        }

        public async Task<ProductModel> AddProductAsync(ProductModel product)
        {
            var response = await _restaurantHttpClient.PostAsync("Product", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductModel>();
        }

        public async Task<ProductModel> UpdateProductAsync(ProductModel product)
        {
            var response = await _restaurantHttpClient.PutAsync($"Product/{product.Id}", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductModel>();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _restaurantHttpClient.DeleteAsync($"Product/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
