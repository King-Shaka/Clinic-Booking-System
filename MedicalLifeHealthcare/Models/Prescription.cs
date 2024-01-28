using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime? DatePrescribed { get; set; } = DateTime.Now;
        [Required]
        public string? MedicationName { get; set; }
        [Required]
        public string? Dosage { get; set; }
        [Required]
        public string? Duration { get; set; }
        public string? Notes { get; set; }

        public string? DoctorsID { get; set; }
        // FK for User
        [ForeignKey(nameof(DoctorsID))]
        public virtual ApplicationUser? Doctor { get; set; }

        public string? PatientId { get; set; } // FK for Patient
        [ForeignKey(nameof(PatientId))]
        public virtual ApplicationUser? Patient { get; set; }
    }
}
