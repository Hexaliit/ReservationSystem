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
            if(category == null)
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
            if (string.IsNullOrEmpty(viewModel.Category.Name))
            {
                ModelState.AddModelError(nameof(viewModel.Category.Name), "Name is required");
            }
            if (string.IsNullOrEmpty(viewModel.Category.Description))
            {
                ModelState.AddModelError(nameof(viewModel.Category.Description), "Decription is required");
            }
            
            if(viewModel.Image == null || viewModel.Image.Length > 2000000)
            {
                ModelState.AddModelError(nameof(viewModel.Image), "Image is required and should be less than 2M");
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var path = imageService.Save(viewModel.Image);
            viewModel.Category.ImagePath = path;
            categoryRepository.AddCategory(viewModel.Category);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(new CategoryViewModel() { Category = category});
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id,  [FromForm] CategoryViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Category.Name))
            {
                ModelState.AddModelError(nameof(viewModel.Category.Name), "Name is required");
            }
            if (string.IsNullOrEmpty(viewModel.Category.Description))
            {
                ModelState.AddModelError(nameof(viewModel.Category.Description), "Decription is required");
            }

            if (viewModel.Image != null && viewModel.Image.Length > 2000000)
            {
                ModelState.AddModelError(nameof(viewModel.Image), "Image should be less than 2M");
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            if(viewModel.Image != null )
            {
                var path = imageService.Save(viewModel.Image);
                category.ImagePath = path;
            }
            category.Name = viewModel.Category.Name;
            category.Description = viewModel.Category.Description;

            categoryRepository.Update(category);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.GetById(id, true);
            if (category == null)
            {
                return NotFound();
            }
            if (category.Menus.Any())
            {
                TempData["Error"] = "This Category Has Some Menu Dependency And Can Not Be Deleted.";
                return Redirect($"/Categories/Get/{id}");
            }
            else {
                categoryRepository.Delete(category);
                imageService.Delete(category.ImagePath);
                return View(nameof(Index));
            }

        }

    }
}
