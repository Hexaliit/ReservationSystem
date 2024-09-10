using ReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Domain.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        public List<Menu> GetAll();
        public Menu? GetById(int id);
        public void Add(Menu menu);
        public void Update(Menu menu);
        public void Delete(Menu menu);
    }
}
