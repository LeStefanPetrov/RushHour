using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
