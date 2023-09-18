namespace MedicalLifeHealthcare.Models.CounsellingModels
{
    public class SessionBooking
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int SessionTypeId { get; set; }

        // Other session booking-related properties

        // Relationships
        public virtual Appointment Appointment { get; set; }
        public virtual SessionType SessionType { get; set; }
    }
}
