using ReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        int[] GetReservationsByDate(DateTime date);
        void Add(Reservation reservation);
    }
}
