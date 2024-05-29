using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepo
    {
        private readonly AppDbContext ctx;
        //private readonly AppUserDbContext userctx;

        public AppointmentRepository(AppDbContext ctx
            //,AppUserDbContext userctx
            )
        {
            this.ctx = ctx;
            //this.userctx = userctx;
        }

        //  **************************************************************
        //  ***** C R E A T E    *****************************************
        //  **************************************************************

        public Task<Result<AppointmentId>> RequestAppointment(Appointment appointment)
        {
            Result<AppointmentId> res;
            var result = ctx.Appointments.Add(appointment);
            if (result == null)
            {
                res = new Result<AppointmentId>(AppointmentId.Empty, false, new Error("Appointment.Request", "Error requesting Appointment"));
            }
            else
            {
                res = new Result<AppointmentId>(result.Entity.Id, true, Error.None);
            }

            return Task.FromResult(res);
        }
        public Task<Result<Appointment?>> ConfirmAppointment(Appointment appointment)
        {
            Result<Appointment?> res;
            var result = ctx.Appointments.Add(appointment);
            if (result == null)
            {
                res = new(null, false, new Error("Appointment.Confirmation", "Error confirming Appointment"));
            }
            else
            {
                res = new(result.Entity, true, Error.None);
            }
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** R E A D   **********************************************
        //  **************************************************************
        public Task<Result<IEnumerable<Appointment>>> GetAllAppointments()
        {
            Result<IEnumerable<Appointment>> res;
            var result = ctx.Appointments.AsNoTracking().AsEnumerable();
            if (result == null || result.Count() == 0)
            {
                res = new(result, false, new Error("Appointment.NotFound", "Could not find any Appointments"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        public Task<Result<Appointment>> GetAppointmentById(AppointmentId id)
        {
            Result<Appointment> res;
            var result = ctx.Appointments.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(result == null)
            {
                res = new(result, false, new Error("Appointment.NotFound", "Could not find Appointment with given Id"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        public Task<Result<IEnumerable<Appointment>>> GetAppointmentsByDay(DateOnly date)
        {
            Result<IEnumerable<Appointment>> res;
            var result = ctx.Appointments.AsNoTracking()
                .Where(d => d.TimeSlot.day == date).AsEnumerable();
            if (result == null || result.Count() == 0)
            {
                res = new(result, false, new Error("Appointment.NotFound", "Could not find any Appointments"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        public Task<Result<IEnumerable<Appointment>>> GetAppointmentsByDateSpan(DateOnly start, DateOnly end)
        {
            Result<IEnumerable<Appointment>> res;
            var result = ctx.Appointments.AsNoTracking()
                .Where(d => d.TimeSlot.day >= start && d.TimeSlot.day <= end).AsEnumerable();
            if (result == null || result.Count() == 0)
            {
                res = new(result, false, new Error("Appointment.NotFound", "Could not find any Appointments"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** U P D A T E    *****************************************
        //  **************************************************************
        public Task<Result<Appointment>> CancelAppointment(AppointmentId id)
        {
            Result<Appointment> res;
            var apmt = ctx.Appointments.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (apmt != null)
            {
                apmt.IsCanceled = true;
                var result = ctx.Appointments.Update(apmt);
                if (result == null)
                {
                    res = new(result!.Entity, false, new Error("AppointmentError.Update", "Could not update given Appointment"));
                }
                else
                {
                    res = new(result.Entity, true, Error.None);
                }
            }
            else
            {
                res = new(null, false, new Error("Appointment.NotFound", "Could not find given Appointment"));
            }
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** D E L E T E    *****************************************
        //  **************************************************************
        public Task<Result<AppointmentId>> DeleteAppointmentById(AppointmentId id)
        {
            Result<AppointmentId> res;
            var found = ctx.Appointments.AsNoTracking().FirstOrDefault(a => a.Id == id);
            var result = found != null ? ctx.Appointments.Remove(found).Entity.Id : AppointmentId.Empty;
            if(result != AppointmentId.Empty)
            {
                res = new(result, true, Error.None);
            }
            else
            {
                res = new(AppointmentId.Empty, false, new Error("Appointment.NotFound", "Could not delete given Appointment"));
            }
            return Task.FromResult(res);
        } 
        public Task<Result<IEnumerable<AppointmentId>>> DeleteAllByDate(DateOnly date) 
        { 
            Result<IEnumerable<AppointmentId>> res;
            List<AppointmentId> ids = new();
            var found = ctx.Appointments.AsNoTracking().Where(c => c.TimeSlot.day == date);
            if(found != null)
            {
                foreach(Appointment value in found) 
                {
                    ids.Add(ctx.Appointments.Remove(value).Entity.Id);
                }
                res = new(ids, true, Error.None);
            }
            else
            {
                res = new(ids, false, new Error("AppointmentError.NotFound", "No Appointments found to delete"));
            }
            return Task.FromResult(res);
        }
        public Task<Result<IEnumerable<AppointmentId>>> DeleteAllBeforeDate(DateOnly date)
        {
            Result<IEnumerable<AppointmentId>> res;
            List<AppointmentId> ids = new();
            var found = ctx.Appointments.AsNoTracking().Where(c => c.TimeSlot.day < date);
            if (found != null)
            {
                foreach (Appointment value in found)
                {
                    ids.Add(ctx.Appointments.Remove(value).Entity.Id);
                }
                res = new(ids, true, Error.None);
            }
            else
            {
                res = new(ids, false, new Error("AppointmentError.NotFound", "No Appointments found to delete"));
            }
            return Task.FromResult(res);
        }

    }
}
