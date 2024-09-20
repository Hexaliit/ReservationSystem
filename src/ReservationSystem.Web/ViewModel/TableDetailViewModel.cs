using ReservationSystem.Web.Models;

namespace ReservationSystem.Web.ViewModel
{
    public class TableDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TableStatus Status { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
    }
}
