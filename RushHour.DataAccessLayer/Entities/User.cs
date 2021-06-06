using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Password { get; set; }
        [Required]
        public Guid RoleID { get; set; }
        public Role Role { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
