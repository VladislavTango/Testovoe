
using MediatR;
using Testovoe.Application.Request.PatientRequest;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Commands.PatientCommands
{
    public class PatientDeleteCommand : IRequestHandler<PatientDeleteRequest, int>
    {
        private readonly ApplicationContext _context;
        public PatientDeleteCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(PatientDeleteRequest request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients.FindAsync(request.Id);

            if (patient == null)
            {
                throw new KeyNotFoundException($"patient not found. {request.Id}");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return request.Id;
        }
    }
}
