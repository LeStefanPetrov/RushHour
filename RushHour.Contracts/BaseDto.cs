using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Contracts
{
    public class BaseDto
    {
        [Required]
        [Key]
        public Guid ID { get; set; }
    }
}
