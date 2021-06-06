using Microsoft.EntityFrameworkCore;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RushHour.DataAccessLayer.Infrastructure
{
    public class RushHourContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentActivity> AppointmentsActivities { get; set; }

        public RushHourContext(DbContextOptions<RushHourContext> options) : base(options)
        {
        }

    }
}
