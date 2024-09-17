using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.Services.ImageService.Interface;
using ReservationSystem.Web.ViewModel;

namespace ReservationSystem.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IImageService imageService;

        public CategoriesController(ICategoryRepository categoryRepository,
            IImageService imageService)
        {
            this.categoryRepository = categoryRepository;
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAllCategories();
            return View(categories);
        }
        public IActionResult Get(int id)
        {
            var category = categoryRepository.GetById(id, true);
            if(category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] CategoryViewModel viewModel)
        {
            var path = imageService.Save(viewModel.Image);
            viewModel.Category.ImagePath = path;
            categoryRepository.AddCategory(viewModel.Category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = categoryRepository.GetById(id);
            if(category is null)
            {
                return NotFound();
            }
            return View(new CategoryViewModel() { Category = category});
        }

        [HttpPost]
        public IActionResult Edit(int id,  [FromForm] CategoryViewModel viewModel)
        {
            var category = categoryRepository.GetById(id);
            if(category is null)
            {
                return NotFound();
            }
            if(viewModel.Image != null)
            {
                var path = imageService.Save(viewModel.Image);
                category.ImagePath = path;
            }
            category.Name = viewModel.Category.Name;
            category.Description = viewModel.Category.Description;

            categoryRepository.Update(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.GetById(id, true);
            if (category is null)
            {
                return NotFound();
            }
            if (category.Menus.Any())
            {
                ViewBag.ErrorMessage = "Dependencies";
                return View();
            }
            else {
                categoryRepository.Delete(category); ;
                return View(nameof(Index));
            }

        }

    }
}
