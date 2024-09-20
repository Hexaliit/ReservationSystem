using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class CategoryDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu> { };
    }
}
