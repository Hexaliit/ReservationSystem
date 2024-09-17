using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class TablesViewModel
    {
        public IEnumerable<Table>? Tables { get; set; }
        public Table? Table { get; set; }
    }
}
