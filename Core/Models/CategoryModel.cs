using System;
using Microsoft.EntityFrameworkCore;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Foodie.Core.Models
{
    public class CategoryModel
    {
        private DataContext dataContext;

        public CategoryModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Category>> GetAll()
        {
            return await dataContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await dataContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            dataContext.Categories.Add(category);
            await dataContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(Category request)
        {
            var category = await dataContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
            category.Name = request.Name;

            await dataContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var category = await dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            dataContext.Categories.Remove(category);
            await dataContext.SaveChangesAsync();

            return category;
        }
    }
}
