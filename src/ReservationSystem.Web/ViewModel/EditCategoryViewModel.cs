using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [Length(3, 30)]
        public string? Name { get; set; }
        [Required]
        [Length(3, 30)]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
