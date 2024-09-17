using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class CategoryViewModel
    {
        public Category Category { get; set; } = new Category();
        public IFormFile? Image { get; set; }
    }
}
