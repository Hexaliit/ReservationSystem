using ReservationSystem.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class AddTAbleViewModel
    {
        [Required]
        [Length(3, 20)]
        public string? Name { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Available;
        [Range(1,20)]
        public int Capacity { get; set; }
        [Required]
        [Length(3, 100)]
        public string? Location { get; set; }
    }
}
