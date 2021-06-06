using Microsoft.EntityFrameworkCore;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(RushHourContext context) : base(context)
        {
        }
 
        public override async Task UpdateAsync(Appointment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
           
            foreach (var activity in entity.Activities)
            {
                if (!_context.AppointmentsActivities.Any(i => i.ActivityID == activity.ActivityID && i.AppointmentID == entity.ID))
                    _context.Entry(activity).State = EntityState.Added;
            }

            var activityIDs = entity.Activities.Select(a => a.ActivityID).ToList();
            var removedAppointmentActivities = await _context.AppointmentsActivities.Where(i => i.AppointmentID == entity.ID && !activityIDs.Contains(i.ActivityID)).ToListAsync();
                _context.AppointmentsActivities.RemoveRange(removedAppointmentActivities);
            _context.Set<Appointment>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task<Appointment> GetByIdAsync(Guid id)
        {
            return await _context.Set<Appointment>().Include(a => a.Activities).ThenInclude(a => a.Activity).FirstOrDefaultAsync(a => a.ID == id);
        }

        public override async Task<IEnumerable<Appointment>> GetPaginatedAsync(int page, int size, Expression<Func<Appointment, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException();

            if (page > 0 && size > 0)
            {
                return await _context.Appointments.Include(a => a.Activities).ThenInclude(a => a.Activity).Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
            }

            else return await _context.Appointments.Include(a => a.Activities).ThenInclude(a => a.Activity).Where(filter).ToListAsync();
        }

        public async Task<bool> CheckAppointmentOwnershipAsync(Guid AppointmentId,Guid UserId)
        {
            if (await _context.Appointments.AnyAsync(i => i.ID == AppointmentId && i.UserID == UserId))
                return true;

            return false;
        }

    }   
}
