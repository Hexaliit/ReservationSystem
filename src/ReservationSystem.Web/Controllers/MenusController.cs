using AutoMapper;
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
        private readonly IMapper mapper;

        public MenusController(ICategoryRepository categoryRepository,
            IMenuRepository menuRepository,
            IImageService imageService,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.menuRepository = menuRepository;
            this.imageService = imageService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var menus = menuRepository.GetAll();
            var menusVM = mapper.Map<List<MenuDetailViewModel>>(menus);
            return View(menusVM);
        }
        public IActionResult Get(int id)
        {
            var menu = menuRepository.GetById(id);
            if(menu == null)
            {
                return NotFound();
            }
            var menuVM = mapper.Map<MenuDetailViewModel>(menu);
            return View(menuVM);
        }
        [Authorize]
        public IActionResult Add()
        {
            var menuViewModel = new AddMenuViewModel()
            {
                Categories = mapper.Map<List<CategoryDetailViewModel>>(categoryRepository.GetAllCategories())
            };
            return View(menuViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] AddMenuViewModel menuViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var menu = mapper.Map<Menu>(menuViewModel);
                var imageUrl = imageService.Save(menuViewModel.Image!);
                menu.ImageUrl = imageUrl;
                menuRepository.Add(menu);
                return RedirectToAction("Index");
            }
            return View(menuViewModel);

        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var menu = menuRepository.GetById(id);
            if(menu == null)
            {
                return NotFound();
            }

            var menuViewModel = mapper.Map<EditMenuViewModel>(menu);
            menuViewModel.Categories = mapper.Map<List<CategoryDetailViewModel>>(categoryRepository.GetAllCategories());
            return View(menuViewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, EditMenuViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var menu = menuRepository.GetById(id);
                if(menu == null)
                {
                    return NotFound();
                }
                mapper.Map(viewModel, menu);
                if(viewModel.Image != null)
                {
                    var path = imageService.Save(viewModel.Image);
                    imageService.Delete(menu.ImageUrl!);
                    menu.ImageUrl = path;
                }
                menuRepository.Update(menu);
                return RedirectToAction("Index");
            }
            return View(viewModel);
            
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
