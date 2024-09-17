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
            if(menu is null)
            {
                return NotFound();
            }
            return View(menu);
        }
        public IActionResult Add()
        {
            var menuViewModel = new MenuViewModel()
            {
                Categories = categoryRepository.GetAllCategories()
            };
            return View(menuViewModel);
        }

        [HttpPost]
        public IActionResult Add([FromForm] MenuViewModel menuViewModel)
        {
            var imageUrl = imageService.Save(menuViewModel.Image!);
            menuViewModel.Menu.ImageUrl = imageUrl;
            menuRepository.Add(menuViewModel.Menu);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var menu = menuRepository.GetById(id);
            if(menu is null)
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

        [HttpPost]
        public IActionResult Edit(int id, MenuViewModel viewModel)
        {
            var menu = menuRepository.GetById(id);
            if(menu is null)
            {
                return NotFound();
            }
            if(viewModel.Image != null)
            {
                var path = imageService.Save(viewModel.Image);
                //imageService.Delete(viewModel.Menu.ImageUrl);
                menu.ImageUrl = path;
            }
            menu.Name = viewModel.Menu.Name;
            menu.Description = viewModel.Menu.Description;
            menu.Price = viewModel.Menu.Price;
            menu.CategoryId = viewModel.Menu.CategoryId;



            menuRepository.Update(menu);
            return RedirectToAction("Index");
            
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var menu = menuRepository.GetById(id);
            if (menu is null)
            {
                return NotFound();
            }
            //imageService.Delete(menu.ImageUrl!);
            menuRepository.Delete(menu);
            return RedirectToAction("Index");
        }

    }
}
