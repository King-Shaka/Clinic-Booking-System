using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalLifeHealthcare.Models
{
    public class SampleResults
    {
        public int SampleResultsId { get; set; }

        public int SamplesID { get; set; }
        [ForeignKey(nameof(SamplesID))]
        public virtual Samples? Samples { get; set; }

        public DateTime ResultDate { get; set; } = DateTime.Now;
        [Required]
        public string? TestName { get; set; }

        [Required]
        public string? ResultValue { get; set; }
        [Required]
        public string? ReferenceRange { get; set; }
        [Required]
        public string? UnitOfMeasure { get; set; }
        [Required]
        public string? Interpretation { get; set; }
        [Required]
        public string? Comments { get; set; }
        public string? PathologyID { get; set; }
        [ForeignKey(nameof(PathologyID))]
        public virtual ApplicationUser? Pathology { get; set; }
    }
}
