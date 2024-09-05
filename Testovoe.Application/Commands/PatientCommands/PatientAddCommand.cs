
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Request.PatientRequest;
using Testovoe.Application.Response.PatientResponse;
using Testovoe.Infrastructure.AppContext;
using Testovoe.Models;

namespace Testovoe.Application.Commands.PatientCommands
{
    public class PatientAddCommand : IRequestHandler<PatientAddRequest , PatientAddResponse>
    {
        private readonly ApplicationContext _context;
        public PatientAddCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PatientAddResponse> Handle(PatientAddRequest request, CancellationToken cancellationToken)
        {
            var region = await _context.Regions
                .FirstOrDefaultAsync(x => x.RegionNumber == request.PatientRegion);

            if (region == null)
            {
                region = new Region
                {
                    RegionNumber = request.PatientRegion
                };
                _context.Regions.Add(region);
            }

            var patient = new Patient
            {
                Surname = request.Surname,
                Name = request.Name,
                Patronymic = request.Patronymic,
                Address = request.Address,
                BornTime = request.BornTime,
                Sex = request.Sex,
                PatientRegion = region,
            };
            _context.Add(patient);

            await _context.SaveChangesAsync();

            return new PatientAddResponse { Id = patient.Id };
        }
    }
}
