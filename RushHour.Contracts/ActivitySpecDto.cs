using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Contracts
{
    public class ActivitySpecDto 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Duration { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Price { get; set; }
    }
}
