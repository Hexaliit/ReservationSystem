using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class AddReservationViewModel
    {
        [Required]
        [Length(3,50)]
        public string? FirstName { get; set; }
        [Required]
        [Length(3, 50)]
        public string? lastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Length(10, 11)]
        public string? Phone { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        public int? TableId { get; set; }
    }
}
