
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Doctor.DoctorRequest;
using Testovoe.Application.Doctor.DoctorRespose;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Doctor.DoctorCommands
{
    public class DoctorsListCommand : IRequestHandler<DoctorsListRequest, List<DoctorsListResponse>>
    {
        private readonly ApplicationContext _context;
        public DoctorsListCommand(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorsListResponse>> Handle(DoctorsListRequest request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy
            };

            var query = _context.Doctors
                         .Select(x => new DoctorsListResponse
                         {
                             Id = x.Id,
                             FIO = x.FIO,
                             RoomNumber = x.DoctorsRoom.RoomNumber,
                             SpecializationName = x.Specialization.SpecializationName,
                             Region = x.DoctorsRegion.Id
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
