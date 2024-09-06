using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Infrastructure.AppContext;
using Testovoe.Models;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientRedactCommand : IRequestHandler<PatientRedactRequest, Unit>
    {
        private readonly ApplicationContext _context;
        public PatientRedactCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(PatientRedactRequest request, CancellationToken cancellationToken)
        {
            var RedactPatient = _context.Patients.FirstOrDefault(x => x.Id == request.RedactId);
            if (RedactPatient == null)
            {
                throw new KeyNotFoundException($"Patient not found. {request.RedactId}");
            }

            var region = await _context.Regions
                .FirstOrDefaultAsync(x => x.RegionNumber == request.PatientRegionNumber);

            if (region == null)
            {
                region = new RegionModel
                {
                    RegionNumber = request.PatientRegionNumber
                };
                _context.Regions.Add(region);
            }

            RedactPatient.Id = request.RedactId;
            RedactPatient.Surname = request.Surname;
            RedactPatient.Name = request.Name;
            RedactPatient.Patronymic = request.Patronymic;
            RedactPatient.Address = request.Address;
            RedactPatient.BornTime = request.BornTime;
            RedactPatient.Sex = request.Sex;
            RedactPatient.PatientRegion = region;

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
