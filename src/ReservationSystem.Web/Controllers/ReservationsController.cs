using AutoMapper;
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
        private readonly IMapper mapper;

        public ReservationsController(IHttpContextAccessor httpContext,
            ITableRepository tableRepository,
            IReservationRepository reservationRepository,
            IMapper mapper)
        {
            this.httpContext = httpContext;
            this.tableRepository = tableRepository;
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }
        public IActionResult StepOne()
        {
            var reservation = new AddReservationViewModel();
            ViewBag.minDate = DateTime.Today;
            ViewBag.maxDate = DateTime.Today.AddDays(7);
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if (session != null)
            {
                var value = JsonSerializer.Deserialize<AddReservationViewModel>(session);
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
        public IActionResult StepOne(AddReservationViewModel reservation)
        {
            
            if (ModelState.IsValid)
            {
                var session = httpContext.HttpContext?.Session;
                if (session?.GetString("reservation") == null) 
                {
                    session?.SetString("reservation", JsonSerializer.Serialize(reservation));
                }
                else
                {
                    var value = JsonSerializer.Deserialize<AddReservationViewModel>(session.GetString("reservation"));
                    if(value != null)
                    {
                        value.FirstName = reservation.FirstName;
                        value.lastName = reservation.lastName;
                        value.Email = reservation.Email;
                        value.Phone = reservation.Phone;
                        value.NumberOfGuests = reservation.NumberOfGuests;
                        value.ReservationDate = reservation.ReservationDate;

                        session?.SetString("reservation", JsonSerializer.Serialize(value));
                    }
                    else
                    {
                        return RedirectToAction("StepOne");
                    }
                }
                return RedirectToAction("StepTwo");
            }
            return View(reservation);
        }

        public IActionResult StepTwo()
        {
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if (session != null)
            {
                var value = JsonSerializer.Deserialize<AddReservationViewModel>(session);
                
                var tablesId = reservationRepository.GetReservationsByDate(value!.ReservationDate);
                var tableViewModel = new TablesViewModel()
                {
                    Tables = mapper.Map<List<TableDetailViewModel>>(tableRepository.GetAvailableTables(tablesId, value!.NumberOfGuests))
                };
                return View(tableViewModel);
            }
            else
            {
                return RedirectToAction("StepOne");
            }
        }

        [HttpPost]
        public IActionResult StepTwo(TablesViewModel table)
        {
            var session = httpContext.HttpContext?.Session?.GetString("reservation");
            if(session != null)
            {
                var reservationVM = new AddReservationViewModel();
                var value = JsonSerializer.Deserialize<AddReservationViewModel>(session);
                if(value != null)
                {
                    reservationVM.FirstName = value.FirstName;
                    reservationVM.lastName = value.lastName;
                    reservationVM.Email = value.Email;
                    reservationVM.Phone = value.Phone;
                    reservationVM.NumberOfGuests = value.NumberOfGuests;
                    reservationVM.ReservationDate = value.ReservationDate;
                    reservationVM.TableId = table.Id;
                }

                var reservation = mapper.Map<Reservation>(reservationVM);

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
