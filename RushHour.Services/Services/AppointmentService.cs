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
    public class AppointmentService : BaseService<Appointment, AppointmentDto>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper) : base(appointmentRepository, mapper)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Guid> InsertAsync(AppointmentSpecDto dto)
        {
            var entity = _mapper.Map<AppointmentSpecDto, Appointment>(dto);
            entity.Activities = new List<AppointmentActivity>();

            foreach (var activity in dto.ListOfActivities)
            {
                var appointmentActivity = new AppointmentActivity();
                appointmentActivity.ID = Guid.NewGuid();
                appointmentActivity.AppointmentID = entity.ID;
                appointmentActivity.ActivityID = activity.ID;

                entity.Activities.Add(appointmentActivity);
            }
            await _appointmentRepository.InsertAsync(entity);
            
            return entity.ID;
        }

        public override async Task UpdateAsync(AppointmentDto dto)
        {
            var entity = _mapper.Map<AppointmentDto, Appointment>(dto);
            entity.Activities = new List<AppointmentActivity>();

            foreach (var activity in dto.ListOfActivities)
            {
                var appointmentActivity = new AppointmentActivity();
                appointmentActivity.ID = Guid.NewGuid();
                appointmentActivity.AppointmentID = entity.ID;
                appointmentActivity.ActivityID = activity.ID;

                entity.Activities.Add(appointmentActivity);
            }
            await _appointmentRepository.UpdateAsync(entity);
        }

        public async Task<List<AppointmentDto>> GetPaginatedAppointmentsAsync(int page, int size, Guid? userId)
        {
            Expression<Func<Appointment, bool>> filter;
            if(userId != null)
                filter = u => u.UserID.Equals(userId);
             else filter = u => true;

            var entities = await _appointmentRepository.GetPaginatedAsync(page, size, filter);
            return _mapper.Map<List<Appointment>, List<AppointmentDto>>(entities.ToList());
        }

        public async Task<bool> CheckAppointmentOwnershipAsync(Guid appointmentId, Guid userId) 
        {
            return await _appointmentRepository.CheckAppointmentOwnershipAsync(appointmentId, userId);
        }

    }
}
