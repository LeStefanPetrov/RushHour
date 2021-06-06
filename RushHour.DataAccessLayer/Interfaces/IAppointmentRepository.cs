using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<bool> CheckAppointmentOwnershipAsync(Guid AppointmentId, Guid UserId);
    }
}
