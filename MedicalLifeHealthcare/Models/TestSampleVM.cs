using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalLifeHealthcare.Models
{
    public class TestSampleVM
    {
        //Request
        public string? PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public virtual ApplicationUser? Patient { get; set; }

        public string? RequestedBy { get; set; } // Nurse or doctor who requested the test
        [ForeignKey(nameof(RequestedBy))]
        public virtual ApplicationUser? Clinician { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string? TestName { get; set; }

        public string? Instructions { get; set; }

        //

        public int TestRequestId { get; set; }
        [ForeignKey(nameof(TestRequestId))]

        public virtual TestRequest? TestRequest { get; set; }// Foreign key linking to the associated test request

        public DateTime? CollectionDate { get; set; }

        public string? CollectorName { get; set; }

        public string? CollectionLocation { get; set; }

        public string? CollectionMethod { get; set; }

        public string? SampleContainerNumber { get; set; }

    }
}
