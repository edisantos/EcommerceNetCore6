using lemossolucoestecnologia.ecommerce.Data.Contexto;
using lemossolucoestecnologia.ecommerce.Domain.Entities;
using lemossolucoestecnologia.ecommerce.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Repositories
{
    public class ProductsRepository : IProductsServices
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public ProductsRepository(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }
        public async Task Commit()
        {
           await  _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            var prod = _context.Products.Find(Id);
            if (prod != null)
            {
                _context.Products.Remove(prod);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Products>> GetProductsByName(Products productName)
        {
            var result = _context.Products.Where(x => x.ProductName == productName.ProductName).ToList();
            return result;
        }

        public async Task Register(Products products)
        {
            _context.Add(products);
        }

        public Task Update(Products products)
        {
            throw new NotImplementedException();
        }
    }
}
