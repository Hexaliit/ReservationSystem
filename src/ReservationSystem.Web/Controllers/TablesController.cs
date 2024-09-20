using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.ViewModel;

namespace ReservationSystem.Web.Controllers
{
    [Authorize]
    public class TablesController : Controller
    {
        
        private readonly ITableRepository tableRepository;
        private readonly IMapper mapper;

        public TablesController(ITableRepository tableRepository,
            IMapper mapper)
        {
            this.tableRepository = tableRepository;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var tables = tableRepository.GetAll();
            var tablesVM = mapper.Map<List<TableDetailViewModel>>(tables);
            return View(tablesVM);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddTAbleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var table = mapper.Map<Table>(viewModel);
                tableRepository.Add(table);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var table = tableRepository.GetById(id);
            if(table == null)
            {
                return NotFound();
            }
            var tableVM = mapper.Map<EditTableViewModel>(table);
            return View(tableVM);
        }

        [HttpPost]
        public IActionResult Update(int id, EditTableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var table = tableRepository.GetById(id);
                if(table == null)
                {
                    return NotFound();
                }
                mapper.Map(viewModel, table);
                tableRepository.Update(table);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
