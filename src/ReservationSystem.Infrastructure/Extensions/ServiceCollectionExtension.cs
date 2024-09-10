using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Infrastructure.Persistence.DbContextes;
using ReservationSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ReservationConnectionString")));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
        }
    }
}
