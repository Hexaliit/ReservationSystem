using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Enums;

namespace ReservationSystem.Web.ViewModel
{
    public class TableViewModel
    {
        public Table Table { get; set; }
        public TableStatus TableStatus { get; set; }
    }
}
