using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? lastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int TableId { get; set; }
        public Table? Table { get; set; }

    }
}
