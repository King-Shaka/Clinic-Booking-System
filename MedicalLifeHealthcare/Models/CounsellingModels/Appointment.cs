using System.ComponentModel.DataAnnotations;

namespace MedicalLifeHealthcare.Models.CounsellingModels
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int CounselorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsConfirmed { get; set; }
        public string Notes { get; set; }

        // Relationships
        public virtual Patient Patient { get; set; }
        public virtual Counselor Counselor { get; set; }
        public virtual ICollection<SessionBooking> SessionBookings { get; set; }
        // Other relationships as needed
    }

}
