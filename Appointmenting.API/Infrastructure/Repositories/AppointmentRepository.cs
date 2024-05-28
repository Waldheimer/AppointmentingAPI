using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Database;

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
            if(result == null)
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


        //  **************************************************************
        //  ***** U P D A T E    *****************************************
        //  **************************************************************


        //  **************************************************************
        //  ***** D E L E T E    *****************************************
        //  **************************************************************
    }
}
