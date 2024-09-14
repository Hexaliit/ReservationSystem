using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Enums;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Infrastructure.Persistence.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TableRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Table table)
        {
            dbContext.Add(table);
            dbContext.SaveChanges();
        }


        public void Delete(Table table)
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAll()
        {
            return dbContext.Tables.ToList();
        }

        public List<Table>? GetAvailableTables(int[] tablesId, int numberOfGuests)
        {
            return dbContext.Tables.Where(t => t.Status == TableStatus.Available && t.Capacity > numberOfGuests && !tablesId.Contains(t.Id)).ToList();
        }

        public Table? GetById(int id)
        {
            return dbContext.Tables.FirstOrDefault(t => t.Id == id);
        }

        public void Update(Table table)
        {
            dbContext.Tables.Update(table);
            dbContext.SaveChanges();
        }
    }
}
