using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Contracts
{
    public class UserRoleDto : BaseDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
