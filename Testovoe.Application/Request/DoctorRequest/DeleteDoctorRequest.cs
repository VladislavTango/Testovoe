using MediatR;


namespace Testovoe.Application.Request.DoctorRequest
{
    public class DeleteDoctorRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
