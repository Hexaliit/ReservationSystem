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
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MenuRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Menu menu)
        {
            dbContext.Menus.Add(menu);
            dbContext.SaveChanges();
        }
        public void Delete(Menu menu)
        {
            dbContext.Menus.Remove(menu);
            dbContext.SaveChanges();
        }

        public List<Menu> GetAll()
        {
            var menus = dbContext.Menus.ToList();
            return menus;
        }

        public Menu? GetById(int id)
        {
            var menu = dbContext.Menus.FirstOrDefault(c => c.Id == id);
            return menu;
        }

        public void Update(Menu menu)
        {
            dbContext.Menus.Update(menu);
            dbContext.SaveChanges();
        }
    }
}
