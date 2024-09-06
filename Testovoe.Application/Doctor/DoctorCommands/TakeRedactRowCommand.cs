
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class TakeRedactRowCommand : IRequestHandler<TakeReadactRowRequest, TakeRedactRowResponse>
    {
        private readonly ApplicationContext _context;

        public TakeRedactRowCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TakeRedactRowResponse> Handle(TakeReadactRowRequest request, CancellationToken cancellationToken)
        {
            var doctor = _context.Doctors
                .Include(x => x.DoctorsRoom)
                .Include(x => x.Specialization)
                .Include(x => x.DoctorsRegion)
                .FirstOrDefault(x => x.Id == request.Id);

            if (doctor == null)
            {
                throw new KeyNotFoundException($"Doctor not found. {request.Id}");
            }

            return new TakeRedactRowResponse
            {
                DoctorID = request.Id,
                DoctorsRegionID = doctor.DoctorsRegion.Id,
                DoctorsRoomId = doctor.DoctorsRoom.Id,
                SpecializationID = doctor.Specialization.Id,
                FIO = doctor.FIO
            };
        }
    }
}
