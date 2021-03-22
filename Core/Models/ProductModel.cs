using System;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Foodie.Core.Models
{
    public class ProductModel
    {
        private DataContext dataContext;

        public ProductModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Product>> GetAll()
        {
            return await dataContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            dataContext.Products.Add(product);
            await dataContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(Product request)
        {
            var product = await dataContext.Products.FirstOrDefaultAsync(c => c.Id == request.Id);
            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            await dataContext.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await dataContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            dataContext.Products.Remove(product);
            await dataContext.SaveChangesAsync();
        }
    }
}
