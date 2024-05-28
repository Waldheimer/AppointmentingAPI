using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Application.RepositoryContracts
{
    public interface IAppointmentRepo
    {
        Task<Result<AppointmentId>> RequestAppointment(Appointment dto);
        Task<Result<Appointment?>> ConfirmAppointment(Appointment dto);
    }
}
