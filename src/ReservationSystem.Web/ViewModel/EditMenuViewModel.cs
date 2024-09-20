using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Web.ViewModel
{
    public class EditMenuViewModel
    {
        public IEnumerable<CategoryDetailViewModel> Categories { get; set; } = new List<CategoryDetailViewModel>();
        public int Id { get; set; }
        [Required]
        [Length(3, 50)]
        public string? Name { get; set; }
        [Required]
        [Length(3, 150)]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        [Range(1, 1000000000)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
