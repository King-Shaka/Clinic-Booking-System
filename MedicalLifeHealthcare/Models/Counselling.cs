namespace MedicalLifeHealthcare.Models
{
    public class Counselling
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string TherapistName { get; set; }
        public string PatientName { get; set; }
        public string PatientContact { get; set; }
        public string TherapistContact { get; set; }
        public string SessionNotes { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsFollowUpRequired { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; }
    }
}
