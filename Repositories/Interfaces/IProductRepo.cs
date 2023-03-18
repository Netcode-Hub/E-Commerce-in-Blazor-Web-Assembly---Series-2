using ORMExplained.Shared.DTO;
using ORMExplained.Shared.Models;

namespace ORMExplained.Server.Repositories.Interfaces
{
    public interface IProductRepo
    {
        public Task<ServiceModel<Product>> AddProduct(Product NewProduct);
        public Task<ServiceModel<Product>> UpdateProduct(Product NewProduct);
        public Task<ServiceModel<Product>> GetProducts();
        public Task<ServiceModel<Product>> GetProduct(int ProductId);
        public Task<ServiceModel<Product>> DeleteProduct(int ProductId);
    }
}
