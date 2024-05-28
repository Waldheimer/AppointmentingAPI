using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler
{
    public class RequestAppointmentCommandHandler : IRequestHandler<RequestAppointmentCommand, Result<AppointmentId>>
    {
        private readonly IAppointmentRepo _repo;
        private readonly IUnitOfWork _unit;

        public RequestAppointmentCommandHandler(IAppointmentRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<AppointmentId>> Handle(RequestAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.RequestAppointment(request.Appointment);
            if(result != null)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result!;
        }
    }
}
