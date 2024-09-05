using MediatR;
using Testovoe.Application.Response.DoctorRespose;

namespace Testovoe.Application.Request.DoctorRequest
{
    public class TakeReadactRowRequest : IRequest<TakeRedactRowResponse>
    {
        public int Id {  get; set; }
    }
}
