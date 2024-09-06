
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Infrastructure.AppContext;
using Testovoe.Models;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DoctorRedactCommand : IRequestHandler<DoctorRedactRequest, Unit>
    {
        private readonly ApplicationContext _context;
        public DoctorRedactCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DoctorRedactRequest request, CancellationToken cancellationToken)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == request.RedactId);

            if (doctor == null)
            {
                throw new KeyNotFoundException($"Doctor not found. {request.RedactId}");
            }

            var room = await _context.DoctorsRooms
                .FirstOrDefaultAsync(x => x.RoomNumber == request.DoctorsRoom);
            var spec = await _context.Specializations
                .FirstOrDefaultAsync(x => x.SpecializationName == request.Specialization);
            var region = await _context.Regions
                .FirstOrDefaultAsync(x => x.RegionNumber == request.DoctorsRegion);

            if (spec == null)
            {
                spec = new SpecializationModel
                {
                    SpecializationName = request.Specialization
                };
                _context.Specializations.Add(spec);
            }

            if (region == null)
            {
                region = new RegionModel
                {
                    RegionNumber = request.DoctorsRegion
                };
                _context.Regions.Add(region);
            }



            var oldRoom = _context.DoctorsRooms.FirstOrDefault(x => x.RoomNumber == doctor.DoctorsRoomId);

            oldRoom.Doctor.Remove(doctor);

            await _context.SaveChangesAsync();

            doctor.FIO = request.FIO;
            doctor.DoctorsRegion = region;
            doctor.DoctorsRoom = room;
            doctor.Specialization = spec;

            if (room == null)
            {
                room = new DoctorsRoomModel
                {
                    RoomNumber = request.DoctorsRoom,
                    Doctor = new List<DoctorModel> { doctor }
                };
                _context.DoctorsRooms.Add(room);
            }
            else
            {
                room.Doctor.Add(doctor);
            }
            return Unit.Value;

        }
    }
}
