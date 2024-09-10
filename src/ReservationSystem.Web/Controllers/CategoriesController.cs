using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Interfaces.Repositories;

namespace ReservationSystem.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAllCategories();
            return View(categories);
        }
        public IActionResult Get(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            if(category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            categoryRepository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
