using AutoMapper;
using ReservationSystem.Web.Models;
using ReservationSystem.Web.ViewModel;

namespace ReservationSystem.Web.Services.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<AddTAbleViewModel, Table>();
            CreateMap<AddCategoryViewModel, Category>();
            CreateMap<EditCategoryViewModel, Category>();
            CreateMap<Category, EditCategoryViewModel>();
            CreateMap<Category,CategoryDetailViewModel>();
            CreateMap<Menu, MenuDetailViewModel>();
            CreateMap<AddMenuViewModel, Menu>();
            CreateMap<Menu, AddMenuViewModel>();
            CreateMap<EditMenuViewModel, Menu>();
            CreateMap<EditTableViewModel, Table>();
            CreateMap<Table,EditTableViewModel>();
            CreateMap<Table, TableDetailViewModel>();
            CreateMap<Menu, EditMenuViewModel>();
            CreateMap<AddReservationViewModel, Reservation>();
        }
    }
}
