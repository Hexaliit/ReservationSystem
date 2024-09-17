using ReservationSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Web.Models.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category? GetById(int id, bool includeMenu = false);
        public void AddCategory(Category category);
        public void Update(Category category);
        public void Delete(Category category);
    }
}
