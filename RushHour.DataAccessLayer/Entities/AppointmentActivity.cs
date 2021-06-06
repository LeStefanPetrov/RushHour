using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class AppointmentActivity : BaseEntity
    {
        [Required]
        public Guid AppointmentID { get; set; }
        [Required]
        public Guid ActivityID { get; set; }
        public Appointment Appointment { get; set; }
        public Activity Activity { get; set; }
    }
}
