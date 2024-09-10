using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Web.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace ReservationSystem.Web.Controllers
{
    public class MenusController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMenuRepository menuRepository;

        public MenusController(ICategoryRepository categoryRepository,
            IMenuRepository menuRepository)
        {
            this.categoryRepository = categoryRepository;
            this.menuRepository = menuRepository;
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
        public IActionResult Edit(int id)
        {
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
            menu.Name = viewModel.Menu.Name;
            menu.Description = viewModel.Menu.Description;
            menu.Price = viewModel.Menu.Price;
            menu.ImageUrl = viewModel.Menu.ImageUrl;
            menu.Price = viewModel.Menu.Price;

            menuRepository.Update(menu);
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public IActionResult Add(MenuViewModel menuViewModel)
        {
            menuRepository.Add(menuViewModel.Menu);
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
            menuRepository.Delete(menu);
            return RedirectToAction("Index");
        }

    }
}
