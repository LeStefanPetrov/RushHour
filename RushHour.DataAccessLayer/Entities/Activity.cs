using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class Activity : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<AppointmentActivity> Appointments { get; set; }
    }
}
