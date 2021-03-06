using RushHour.Contracts;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Interfaces
{
    public interface IActivityService : IBaseService<Activity, ActivityDto>
    {
        Task<List<ActivityDto>> GetPaginatedActivitiesAsync(int page, int size, string name);
    }
}
