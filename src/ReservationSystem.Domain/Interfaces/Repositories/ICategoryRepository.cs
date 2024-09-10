using ReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category? GetCategoryById(int id);
        public void AddCategory(Category category);
        public void UpdateCategory(int categoryId, Category category);
        public void DeleteCategory(Category category);
    }
}
