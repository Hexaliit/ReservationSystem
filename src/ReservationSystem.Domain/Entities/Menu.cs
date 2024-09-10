using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
    }
}
