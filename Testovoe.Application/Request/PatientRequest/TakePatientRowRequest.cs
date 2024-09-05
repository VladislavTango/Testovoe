using MediatR;
using Testovoe.Application.Response.PatientResponse;

namespace Testovoe.Application.Request.PatientRequest
{
    public class TakePatientRowRequest : IRequest<TakePatientRowResponse>
    {
        public int id {  get; set; }
    }
}
