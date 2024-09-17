using ReservationSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Web.Models.Repositories.Interface
{
    public interface ITableRepository
    {
        List<Table> GetAll();
        List<Table>? GetAvailableTables(int[] tablesId, int numberOfGuests);
        Table? GetById(int id);
        void Add(Table table);
        void Update(Table table);
        void Delete(Table table);
    }
}
