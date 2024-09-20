using ReservationSystem.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class EditTableViewModel
    {
        public int Id { get; set; }
        [Required]
        [Length(3,50)]
        public string? Name { get; set; }
        [Required]
        public TableStatus Status { get; set; }
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }
        [Required]
        [Length(3, 150)]
        public string? Location { get; set; }
    }
}
