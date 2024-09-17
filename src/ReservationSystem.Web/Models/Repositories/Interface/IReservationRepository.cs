using ReservationSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Web.Models.Repositories.Interface
{
    public interface IReservationRepository
    {
        int[] GetReservationsByDate(DateTime date);
        void Add(Reservation reservation);
    }
}
