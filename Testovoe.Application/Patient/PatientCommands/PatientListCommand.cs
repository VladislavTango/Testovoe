
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Patient.PatientRequest;
using Testovoe.Application.Patient.PatientResponse;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Patient.PatientCommands
{
    public class PatientListCommand : IRequestHandler<PatientListRequest, List<PatientListResponse>>
    {
        private readonly ApplicationContext _context;
        public PatientListCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<PatientListResponse>> Handle(PatientListRequest request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy
            };

            var query = _context.Patients
                        .Select(x => new PatientListResponse
                        {
                            Id = x.Id,
                            Surname = x.Surname,
                            Name = x.Name,
                            Patronymic = x.Patronymic,
                            Address = x.Address,
                            BornTime = x.BornTime,
                            Sex = x.Sex,
                            PatientRegionNumber = x.PatientRegion.RegionNumber
                        });

            var filteredAndSortedQuery = query
                .ApplyFiltering(gridifyQuery).ApplyOrdering(gridifyQuery);

            var paginatedResult = await filteredAndSortedQuery
                .Skip((request.Page - 1) * 20)
                .Take(20)
                .ToListAsync();

            return paginatedResult;
        }
    }
}
