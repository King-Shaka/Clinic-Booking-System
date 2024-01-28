using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalLifeHealthcare.Models
{
    public class Samples
    {
        [Key]
        public int SampleCollectionId { get; set; }

        public int TestRequestId { get; set; }
        [ForeignKey(nameof(TestRequestId))]
       
        public virtual TestRequest? TestRequest { get; set; }// Foreign key linking to the associated test request

        public DateTime? CollectionDate { get; set; } = DateTime.Now;

        public string? CollectorName { get; set; }
        [ForeignKey(nameof(CollectorName))]
        public virtual ApplicationUser? Nurse { get; set; }

        public string? CollectionLocation { get; set; }

        public string? CollectionMethod { get; set; }

        public string? SampleContainerNumber { get; set; }

        // Other relevant fields

        // Navigation property for related test request
       
    }
}
