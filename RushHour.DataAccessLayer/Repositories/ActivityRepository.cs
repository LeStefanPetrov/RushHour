using Microsoft.EntityFrameworkCore;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(RushHourContext context) : base(context)
        {
        }

        public int GetDuration (List<ActivityDto> activities)
        {
            var guids = activities.Select(x => x.ID).ToList();
            return  _context.Activities.Where(x => guids.Contains(x.ID)).Sum(d => d.Duration);
        }


    }
}
