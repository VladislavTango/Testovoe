using Testovoe.Infrastructure.AppContext;
using MediatR;
using Testovoe.Models;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Request.DoctorRequest;
using Testovoe.Application.Response.DoctorRespose;

namespace Testovoe.Application.Commands.DoctorCommands
{
    public class AddDoctorCommand : IRequestHandler<AddDoctorRequest, AddDoctorResponse>
    {
        private readonly ApplicationContext _context;
        public AddDoctorCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AddDoctorResponse> Handle(AddDoctorRequest request, CancellationToken cancellationToken)
        {
            var room = await _context.DoctorsRooms
                .FirstOrDefaultAsync(x => x.RoomNumber == request.RoomNumber);
            var spec = await _context.Specializations
                .FirstOrDefaultAsync(x => x.SpecializationName == request.Specialization);
            var region = await _context.Regions
                .FirstOrDefaultAsync(x => x.RegionNumber == request.Region);

            if (spec == null)
            {
                spec = new Specialization
                {
                    SpecializationName = request.Specialization
                };
                _context.Specializations.Add(spec);
            }

            if (region == null)
            {
                region = new Region
                {
                    RegionNumber = request.Region
                };
                _context.Regions.Add(region);
            }

            var newDoctor = new Doctor
            {
                FIO = request.FIO,
                Specialization = spec,
                DoctorsRegion = region,
                DoctorsRoom = room
            };

            if (room == null)
            {
                room = new DoctorsRoom
                {
                    RoomNumber = request.RoomNumber,
                    Doctor = new List<Doctor> { newDoctor }
                };
                _context.DoctorsRooms.Add(room);
            }
            else
            {
                room.Doctor.Add(newDoctor);
            }

            await _context.SaveChangesAsync();

            return new AddDoctorResponse
            {
                Id = newDoctor.Id,
            };
        }

    }
}
