using MediatR;
using Testovoe.Application.Response.DoctorRespose;

namespace Testovoe.Application.Request.DoctorRequest
{
    public class DoctorsListRequest : IRequest<List<DoctorsListResponse>>
    {
        public string SortBy { get; set; }
        public int Page { get; set; }

    }
}
