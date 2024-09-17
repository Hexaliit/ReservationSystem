using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Infrastructure.Persistence.Configurations
{
    public class RoleTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = Convert.ToString(DateTime.Now) },
                new Role() { Name = "User", NormalizedName = "User", ConcurrencyStamp = Convert.ToString(DateTime.Now) }
                );
        }
    }
}
