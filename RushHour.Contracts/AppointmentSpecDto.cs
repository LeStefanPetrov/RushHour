using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Contracts
{
    public class AppointmentSpecDto 
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public List<ActivityDto> ListOfActivities { get; set; }
        [Required]
        public Guid UserID { get; set; }
    }
}
