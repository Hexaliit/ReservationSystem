using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.Services.ImageService.Interface;
using ReservationSystem.Web.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace ReservationSystem.Web.Controllers
{
    public class MenusController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IImageService imageService;

        public MenusController(ICategoryRepository categoryRepository,
            IMenuRepository menuRepository,
            IImageService imageService)
        {
            this.categoryRepository = categoryRepository;
            this.menuRepository = menuRepository;
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
            var menus = menuRepository.GetAll();
            return View(menus);
        }
        public IActionResult Get(int id)
        {
            var menu = menuRepository.GetById(id);
            if(menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }
        [Authorize]
        public IActionResult Add()
        {
            var menuViewModel = new MenuViewModel()
            {
                Categories = categoryRepository.GetAllCategories()
            };
            return View(menuViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] MenuViewModel menuViewModel)
        {
            if (string.IsNullOrEmpty(menuViewModel.Menu.Name))
            {
                ModelState.AddModelError(nameof(menuViewModel.Menu.Name), "Name is required");
            }
            if (string.IsNullOrEmpty(menuViewModel.Menu.Description))
            {
                ModelState.AddModelError(nameof(menuViewModel.Menu.Description), "Description is required");
            }
            if (menuViewModel.Menu.Price < 1)
            {
                ModelState.AddModelError(nameof(menuViewModel.Menu.Price), "Price can not be ngative or zero.");
            }
            if (menuViewModel.Image == null || menuViewModel.Image.Length > 2000000)
            {
                ModelState.AddModelError(nameof(menuViewModel.Image), "Image is requred and should be less than 2M");
            }
            if (!ModelState.IsValid)
            {
                return View(menuViewModel);
            }

            var imageUrl = imageService.Save(menuViewModel.Image!);
            menuViewModel.Menu.ImageUrl = imageUrl;
            menuRepository.Add(menuViewModel.Menu);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var menu = menuRepository.GetById(id);
            if(menu == null)
            {
                return NotFound();
            }

            var menuViewModel = new MenuViewModel()
            {
                Categories = categoryRepository.GetAllCategories(),
                Menu = menuRepository.GetById(id) ?? new Menu()
            };
            return View(menuViewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MenuViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Menu.Name))
            {
                ModelState.AddModelError(nameof(viewModel.Menu.Name), "Name is required");
            }
            if (string.IsNullOrEmpty(viewModel.Menu.Description))
            {
                ModelState.AddModelError(nameof(viewModel.Menu.Description), "Description is required");
            }
            if (viewModel.Menu.Price < 1)
            {
                ModelState.AddModelError(nameof(viewModel.Menu.Price), "Price can not be ngative or zero.");
            }
            if (viewModel.Image != null && viewModel.Image.Length > 2000000)
            {
                ModelState.AddModelError(nameof(viewModel.Image), "Image should be less than 2M");
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var menu = menuRepository.GetById(id);
            if(menu == null)
            {
                return NotFound();
            }
            if(viewModel.Image != null)
            {
                var path = imageService.Save(viewModel.Image);
                imageService.Delete(viewModel.Menu.ImageUrl);
                menu.ImageUrl = path;
            }
            menu.Name = viewModel.Menu.Name;
            menu.Description = viewModel.Menu.Description;
            menu.Price = viewModel.Menu.Price;
            menu.CategoryId = viewModel.Menu.CategoryId;

            menuRepository.Update(menu);
            return RedirectToAction("Index");
            
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var menu = menuRepository.GetById(id);
            if (menu ==  null)
            {
                return NotFound();
            }
            imageService.Delete(menu.ImageUrl!);
            menuRepository.Delete(menu);
            return RedirectToAction("Index");
        }

    }
}
