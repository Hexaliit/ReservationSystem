using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class MenuDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
