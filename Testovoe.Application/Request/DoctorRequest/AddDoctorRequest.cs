using MediatR;
using Testovoe.Application.Response.DoctorRespose;

namespace Testovoe.Application.Request.DoctorRequest
{
    public class AddDoctorRequest : IRequest<AddDoctorResponse>
    {
        public string FIO { get; set; }
        public int RoomNumber { get; set; }
        public string Specialization { get; set; }
        public int Region { get; set; }
    }
}
