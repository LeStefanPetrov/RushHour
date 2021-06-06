using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class Appointment : BaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid UserID { get; set; }
        public User User { get; set; }
        public ICollection<AppointmentActivity> Activities { get; set; }
    }
}
