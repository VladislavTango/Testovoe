
namespace Testovoe.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int DoctorsRoomId { get; set; }
        public DoctorsRoom DoctorsRoom { get; set; }
        public Specialization Specialization { get; set; }
        public Region? DoctorsRegion { get; set; }
    }
}
