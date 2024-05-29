using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.Appointments
{
    public class ConfirmAppointmentCommandHandler : IRequestHandler<ConfirmAppointmentCommand, Result<Appointment>>
    {
        private readonly IAppointmentRepo _repo;
        private readonly IUnitOfWork _unit;

        public ConfirmAppointmentCommandHandler(IAppointmentRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<Appointment>> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.ConfirmAppointment(request.Appointment);
            if (result != null)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result!;
        }
    }
}
