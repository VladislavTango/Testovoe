

using MediatR;

namespace Testovoe.Application.Request.PatientRequest
{
    public class PatientDeleteRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
