
using MediatR;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DeleteDoctorCommand : IRequestHandler<DeleteDoctorRequest, int>
    {
        private readonly ApplicationContext _context;

        public DeleteDoctorCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteDoctorRequest request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors.FindAsync(request.Id);

            if (doctor == null)
            {
                throw new KeyNotFoundException($"Doctor not found. {request.Id}");
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return request.Id;
        }
    }
}
