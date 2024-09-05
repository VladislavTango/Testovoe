

using MediatR;
using Testovoe.Application.Response.PatientResponse;

namespace Testovoe.Application.Request.PatientRequest
{
    public class PatientListRequest : IRequest<List<PatientListResponse>>
    {
        public string SortBy { get; set; }
        public int Page { get; set; }
    }
}
