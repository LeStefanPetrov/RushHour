using RushHour.Contracts;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Interfaces
{
    public interface IAppointmentService : IBaseService<Appointment, AppointmentDto>
    {
        Task<Guid> InsertAsync(AppointmentSpecDto dto);
        Task<List<AppointmentDto>> GetPaginatedAppointmentsAsync(int page, int size, Guid? userId);
        Task<bool> CheckAppointmentOwnershipAsync(Guid appointmentId, Guid userId);
    }
}
