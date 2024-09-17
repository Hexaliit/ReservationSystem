using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.Models.Repositories.Interface;
using ReservationSystem.Web.ViewModel;
using System.Text.Json;

namespace ReservationSystem.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly ITableRepository tableRepository;
        private readonly IReservationRepository reservationRepository;

        public ReservationsController(IHttpContextAccessor httpContext,
            ITableRepository tableRepository,
            IReservationRepository reservationRepository)
        {
            this.httpContext = httpContext;
            this.tableRepository = tableRepository;
            this.reservationRepository = reservationRepository;
        }
        public IActionResult StepOne()
        {
            var reservation = new Reservation();
            ViewBag.minDate = DateTime.Today;
            ViewBag.maxDate = DateTime.Today.AddDays(7);
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if (session != null)
            {
                var value = JsonSerializer.Deserialize<Reservation>(session);
                if(value != null)
                {
                    reservation.FirstName = value.FirstName;
                    reservation.lastName = value.lastName;
                    reservation.Email = value.Email;
                    reservation.Phone = value.Phone;
                    reservation.NumberOfGuests = value.NumberOfGuests;
                    reservation.ReservationDate = value.ReservationDate;
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public IActionResult StepOne(Reservation reservation)
        {
            if(!ModelState.IsValid)
            {
                return View(reservation);
            }
            var session = httpContext.HttpContext?.Session;
            if (session?.GetString("reservation") is null) 
            {
                session?.SetString("reservation", JsonSerializer.Serialize(reservation));
            }
            else
            {
                var value = JsonSerializer.Deserialize<Reservation>(session.GetString("reservation"));
                value!.FirstName = reservation.FirstName;
                value.lastName = reservation.lastName;
                value.Email = reservation.Email;
                value.Phone = reservation.Phone;
                value.NumberOfGuests = reservation.NumberOfGuests;
                value.ReservationDate = reservation.ReservationDate;
                value.CreatedDate = reservation.CreatedDate;

                session?.SetString("reservation", JsonSerializer.Serialize(value));
            }
            return RedirectToAction("StepTwo");
        }

        public IActionResult StepTwo()
        {
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if (session != null)
            {
                var value = JsonSerializer.Deserialize<Reservation>(session);
                
                var tablesId = reservationRepository.GetReservationsByDate(value!.ReservationDate);
                var tableViewModel = new TablesViewModel()
                {
                    Tables = tableRepository.GetAvailableTables(tablesId, value!.NumberOfGuests)
                };
                return View(tableViewModel);
            }
            else
            {
                return RedirectToAction("StepOne");
            }
        }

        [HttpPost]
        public IActionResult StepTwo(Table table)
        {
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if(session != null)
            {
                var reservation = new Reservation();
                var value = JsonSerializer.Deserialize<Reservation>(session);
                if(value != null)
                {
                    reservation.FirstName = value.FirstName;
                    reservation.lastName = value.lastName;
                    reservation.Email = value.Email;
                    reservation.Phone = value.Phone;
                    reservation.NumberOfGuests = value.NumberOfGuests;
                    reservation.ReservationDate = value.ReservationDate;
                    reservation.CreatedDate = value.CreatedDate;
                    reservation.TableId = table.Id;
                }

                reservationRepository.Add(reservation);

                httpContext.HttpContext?.Session.Remove("reservation");

                return RedirectToAction("ReservationCompleted");
            }
            return RedirectToAction("StepOne");
        }

        public IActionResult ReservationCompleted()
        {
            return View();
        }
    }
}
