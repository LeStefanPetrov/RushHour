using Microsoft.EntityFrameworkCore;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Infrastructure
{
    public static class DataSeeder
    {
        public static void Seed (RushHourContext context)
        {
            context.Database.Migrate();
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { ID = Guid.Parse("e4132212-d5c7-422a-a7ae-1e00eb4cc793"), RoleName = "User" },
                    new Role { ID = Guid.Parse("ae41bb8b-bf8f-4535-b9d2-fcce9231a64f"), RoleName = "Administrator" }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();

            }
        }
    }
}
