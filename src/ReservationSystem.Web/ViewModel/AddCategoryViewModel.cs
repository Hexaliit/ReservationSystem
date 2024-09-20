using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class AddCategoryViewModel
    {
        [Required]
        [Length(3,20)]
        public string? Name { get; set; }
        [Required]
        [Length(3, 100)]
        public string? Description { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
