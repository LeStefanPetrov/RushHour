using AutoMapper;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Services
{
    public class ActivityService : BaseService<Activity, ActivityDto>, IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityService(IActivityRepository activityRepository, IMapper mapper) : base(activityRepository, mapper)
        {
            _activityRepository = activityRepository;
        }

        public async Task<List<ActivityDto>> GetPaginatedActivitiesAsync(int page, int size,string name)
        {
            Expression<Func<Activity, bool>> filter;
            if (!string.IsNullOrEmpty(name))
                filter = u => u.Name.Equals(name);

            else filter = u => true;

            var entities = await _activityRepository.GetPaginatedAsync(page, size, filter);
            return _mapper.Map<List<Activity>, List<ActivityDto>>(entities.ToList());
        }

    }
}
