
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testovoe.Application.Request.DoctorRequest;
using Testovoe.Application.Response.DoctorRespose;
using Testovoe.Infrastructure.AppContext;

namespace Testovoe.Application.Commands.DoctorCommands
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
