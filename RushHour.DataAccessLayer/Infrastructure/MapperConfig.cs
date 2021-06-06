using AutoMapper;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Infrastructure
{
    public class MapperConfig : Profile
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IActivityRepository _activityRepository;

        public MapperConfig(IRoleRepository roleRepository, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _roleRepository = roleRepository;
            CreateMap<UserDto, User>()
                 .ForMember(u => u.RoleID, opt => opt.MapFrom(o => _roleRepository.GetRoleIdAsync(o.RoleName).Result));
            CreateMap<ActivitySpecDto, Activity>()
                 .ForMember(u => u.ID, opt => opt.MapFrom(i => Guid.NewGuid()));
            CreateMap<ActivityDto, Activity>().ReverseMap();
            CreateMap<AppointmentSpecDto, Appointment>()
                .ForMember(u => u.ID, opt => opt.MapFrom(i => Guid.NewGuid()))
                .ForMember(u => u.EndDate, opt => opt.MapFrom(o => o.StartDate.AddMinutes(_activityRepository.GetDuration(o.ListOfActivities))))
                .ForMember(u => u.Activities, opt => opt.MapFrom(o => o.ListOfActivities));
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(u => u.EndDate, opt => opt.MapFrom(o => o.StartDate.AddMinutes(_activityRepository.GetDuration(o.ListOfActivities))))
                .ForMember(u => u.Activities, opt => opt.MapFrom(o => o.ListOfActivities))
                .ReverseMap();
            CreateMap<ActivityDto, AppointmentActivity>()
                .ForMember(u => u.Activity, opt => opt.MapFrom(o => o))
                .ReverseMap();
        }
    }

}
