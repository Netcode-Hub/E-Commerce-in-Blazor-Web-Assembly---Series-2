using ORMExplained.Client.Services.InterfaceServices;
using ORMExplained.Shared.DTO;
using ORMExplained.Shared.Models;
using System.Net.Http.Json;

namespace ORMExplained.Client.Services.ImplementationServices
{
    public class ProductService : IProductServices
    {
        private readonly HttpClient httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ServiceModel<Product>?> AddProduct(Product NewProduct)
        {
            var product = await httpClient.PostAsJsonAsync("api/Product/Add-Product", NewProduct);
            return await product.Content.ReadFromJsonAsync<ServiceModel<Product>>();
        }

        public async Task<ServiceModel<Product>?> DeleteProduct(int ProductId)
        {
            var result = await httpClient.DeleteFromJsonAsync<ServiceModel<Product>>($"api/Product/{ProductId}");
            return result;
        }

        public async Task<ServiceModel<Product>?> GetProduct(int ProductId)
        {
            var result = await httpClient.GetAsync($"api/Product/Get-Product/{ProductId}");
            return await result.Content.ReadFromJsonAsync<ServiceModel<Product>>();
        }

        public async Task<ServiceModel<Product>?> GetProducts()
        {
            var result = await httpClient.GetAsync("api/Product");
            return await result.Content.ReadFromJsonAsync<ServiceModel<Product>>();
        }

        public async Task<ServiceModel<Product>?> UpdateProduct(Product NewProduct)
        {
            var result = await httpClient.PutAsJsonAsync("api/Product", NewProduct);
            return await result.Content.ReadFromJsonAsync<ServiceModel<Product>>();
        }
    }
}
