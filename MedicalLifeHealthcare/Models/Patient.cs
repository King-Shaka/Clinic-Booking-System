namespace MedicalLifeHealthcare.Models
{
    public class Patient
    {
        public int patientID { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MedicalHistory { get; set; }
        public string PrescriptionHistory { get; set; }
        public DateTime AdmissionTime { get; set; }
        public int MyProperty { get; set; }
        public string PhoneNumber { get; set; }
        public string MyProperty1 { get; set; }

    }
}
