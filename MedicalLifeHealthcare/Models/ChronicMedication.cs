using System.ComponentModel.DataAnnotations;

namespace MedicalLifeHealthcare.Models
{
    public class ChronicMedication
    {
        [Key]
        public int Id { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public int PatientId { get; set; }
    }
}
