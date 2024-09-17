using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.ViewModel;

namespace ReservationSystem.Web.Controllers
{
    public class TablesController : Controller
    {
        private readonly ITableRepository tableRepository;

        public TablesController(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }
        public IActionResult Index()
        {
            var tables = tableRepository.GetAll();
            return View(tables);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Table table)
        {
            tableRepository.Add(table);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var table = tableRepository.GetById(id);
            if(table is null)
            {
                return NotFound();
            }
            return View(table);
        }

        [HttpPost]
        public IActionResult Update(int id, Table table)
        {
            var existingTable = tableRepository.GetById(id);
            if(existingTable is null)
            {
                return NotFound();
            }
            existingTable.Name = table.Name;
            existingTable.Capacity = table.Capacity;
            existingTable.Location = table.Location;
            existingTable.Status = table.Status;
            tableRepository.Update(existingTable);
            return RedirectToAction("Index");
        }
    }
}
