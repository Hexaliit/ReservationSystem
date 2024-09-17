using Microsoft.EntityFrameworkCore;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.Data.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Web.Models.Repositories.Concrete
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

        public void Delete(Category category)
        {
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public Category? GetById(int id, bool includeMenu = false)
        {
            var query = dbContext
                            .Categories
                            .Where(c => c.Id == id);
            if (includeMenu)
            {
                query = query.Include(c => c.Menus);
                            
            }

            var category = query.FirstOrDefault();

            return category;
        }

        public void Update(Category category)
        {
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
        }
    }
}
