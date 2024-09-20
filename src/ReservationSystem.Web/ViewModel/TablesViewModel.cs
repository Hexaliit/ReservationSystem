using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class TablesViewModel
    {
        public IEnumerable<TableDetailViewModel>? Tables { get; set; }
        //public TableDetailViewModel? Table { get; set; }
        public int Id { get; set; }
    }
}
