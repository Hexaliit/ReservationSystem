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
