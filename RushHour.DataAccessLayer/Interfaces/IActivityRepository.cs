using RushHour.Contracts;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Interfaces
{
    public interface IActivityRepository : IBaseRepository<Activity>
    {
        int GetDuration(List<ActivityDto> activities);
    }
}
