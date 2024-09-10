using Microsoft.EntityFrameworkCore;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Infrastructure.Persistence.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddCategory(Category category)
        {
            dbContext.Add(category);
            dbContext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public Category? GetCategoryById(int id)
        {
            var category = dbContext
                            .Categories
                            .Where(c => c.Id == id)
                            .Include(c => c.Menus)
                            .FirstOrDefault();

            return category;
        }

        public void UpdateCategory(int categoryId, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
