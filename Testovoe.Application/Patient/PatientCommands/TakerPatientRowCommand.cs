using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Application.Patient.PatientResponse;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class TakerPatientRowCommand : IRequestHandler<TakePatientRowRequest, TakePatientRowResponse>
    {
        private readonly ApplicationContext _context;
        public TakerPatientRowCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TakePatientRowResponse> Handle(TakePatientRowRequest request, CancellationToken cancellationToken)
        {
            var patient = _context.Patients
                .Include(x => x.PatientRegion)
                .FirstOrDefault(x => x.Id == request.id);

            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient not found. {request.id}");
            }

            return new TakePatientRowResponse
            {
                Id = request.id,
                Surname = patient.Surname,
                Name = patient.Name,
                Patronymic = patient.Patronymic,
                Address = patient.Address,
                BornTime = patient.BornTime,
                Sex = patient.Sex,
                PatientRegionID = patient.PatientRegion.Id
            };
        }
    }
}
