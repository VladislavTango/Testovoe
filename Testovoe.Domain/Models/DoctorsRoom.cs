
namespace Testovoe.Models
{
    public class DoctorsRoom
    {
        public int Id { get; set; }
        public int RoomNumber {  get; set; }
        public List<Doctor> Doctor { get; set; } = new List<Doctor>();
        public int DoctorId { get; set; }
    }
}
