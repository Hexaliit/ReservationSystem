using ReservationSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Web.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Available;
        public int Capacity { get; set; }
        public string? Location { get; set; }
    }
}
