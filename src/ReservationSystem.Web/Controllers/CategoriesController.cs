using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Constants;
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
        private readonly IMapper mapper;

        public CategoriesController(ICategoryRepository categoryRepository,
            IImageService imageService,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.imageService = imageService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAllCategories();
            var categoriesVM = mapper.Map<List<CategoryDetailViewModel>>(categories);
            return View(categoriesVM);
        }
        public IActionResult Get(int id)
        {
            var category = categoryRepository.GetById(id, true);
            if(category == null)
            {
                return NotFound();
            }
            var categoryVM = mapper.Map<CategoryDetailViewModel>(category);
            return View(categoryVM);
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(viewModel);
                var path = imageService.Save(viewModel.Image!);
                category.ImagePath = path;
                categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            var categoryVM = mapper.Map<EditCategoryViewModel>(category);
            return View(categoryVM);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id,  [FromForm] EditCategoryViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var category = categoryRepository.GetById(id);
                if(category == null)
                {
                    return NotFound();
                }
                mapper.Map(viewModel, category);
                if (viewModel.Image != null )
                {
                    var path = imageService.Save(viewModel.Image);
                    category.ImagePath = path;
                }
                
                categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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
                TempData["Error"] = Messages.Dependency;
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
