using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class TestRequest
    {
        [Key]
        public int TestRequestId { get; set; }

        public string? PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public virtual ApplicationUser? Patient { get; set; }

        public string? RequestedBy { get; set; } // Nurse or doctor who requested the test
        [ForeignKey(nameof(RequestedBy))]
        public virtual ApplicationUser? Clinician { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string? TestName { get; set; }

        public string? Instructions { get; set; }

        public string? Status { get; set; } = "Pending";

        // Other relevant fields such as test results, reference numbers, etc.

        // Navigation property for related patient
       
    }

}
