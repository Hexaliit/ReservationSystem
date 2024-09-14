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
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ReservationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Reservation reservation)
        {
            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();
        }

        public int[] GetReservationsByDate(DateTime date)
        {
            return dbContext.Reservations
                .Where(r => r.ReservationDate == date)
                .Select(r => r.TableId)
                .ToArray();
        }
    }
}
