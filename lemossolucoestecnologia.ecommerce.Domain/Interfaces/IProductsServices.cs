using lemossolucoestecnologia.ecommerce.Domain.Entities;

namespace lemossolucoestecnologia.ecommerce.Domain.Interfaces
{
    public interface IProductsServices
    {
        Task Register(Products products);
        Task<IEnumerable<Products>> GetProductsByName(Products productName);
        void Delete(int Id);
        Task Update(Products products);
        Task Commit();
    }
}
