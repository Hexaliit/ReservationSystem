using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Web.ViewModel
{
    public class MenuViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public Menu Menu { get; set; } = new Menu();
    }
}
