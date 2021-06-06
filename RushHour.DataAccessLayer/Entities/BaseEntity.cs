using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public Guid ID { get; set; }
    }
}
