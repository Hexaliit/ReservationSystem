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
            if(string.IsNullOrEmpty(table.Name) || table.Name.Length > 50)
            {
                ModelState.AddModelError(nameof(table.Name), "Name is required and should be less than 50 characters");
            }
            if (string.IsNullOrEmpty(table.Location) || table.Location.Length > 50)
            {
                ModelState.AddModelError(nameof(table.Location), "Name is required and should be less than 50 characters");
            }
            if (table.Capacity > 20)
            {
                ModelState.AddModelError(nameof(table.Capacity), "Capacity must be less than 20");
            }
            if (!ModelState.IsValid)
            {
                return View(table);
            }
            tableRepository.Add(table);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var table = tableRepository.GetById(id);
            if(table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        [HttpPost]
        public IActionResult Update(int id, Table table)
        {
            if (string.IsNullOrEmpty(table.Name) || table.Name.Length > 50)
            {
                ModelState.AddModelError(nameof(table.Name), "Name is required and should be less than 50 characters");
            }
            if (string.IsNullOrEmpty(table.Location) || table.Location.Length > 50)
            {
                ModelState.AddModelError(nameof(table.Location), "Name is required and should be less than 50 characters");
            }
            if (table.Capacity > 20)
            {
                ModelState.AddModelError(nameof(table.Capacity), "Capacity must be less than 20");
            }
            if (!ModelState.IsValid)
            {
                return View(table);
            }
            var existingTable = tableRepository.GetById(id);
            if(existingTable == null)
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
