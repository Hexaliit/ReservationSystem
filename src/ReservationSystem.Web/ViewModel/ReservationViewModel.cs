using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Web.ViewModel
{
    public class ReservationViewModel
    {
        public Reservation? Reservation { get; set; }
        public List<Table>? Tables { get; set; }
    }
}
