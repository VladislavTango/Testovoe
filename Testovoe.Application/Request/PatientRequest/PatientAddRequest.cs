using MediatR;
using Testovoe.Application.Response.PatientResponse;
using Testovoe.Enums;

namespace Testovoe.Application.Request.PatientRequest
{
    public class PatientAddRequest : IRequest<PatientAddResponse>
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime BornTime { get; set; }
        public Sex Sex { get; set; }
        public int PatientRegion { get; set; }
    }
}
