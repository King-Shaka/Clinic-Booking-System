using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalLifeHealthcare.Models
{
    public class Case
    {
        [Key]
        public int CaseId { get; set; }
        public DateTime DateOpened { get; set; } = DateTime.Now;

        // Referring to the previously discussed incident report
        public int IncidentReportId { get; set; }
        [ForeignKey(nameof(IncidentReportId))]
        public virtual IncidentReport? incidentReport { get; set; }

        public CaseStatus Status { get; set; } = CaseStatus.Open;
        public string? DoctorsID { get; set; }
        [ForeignKey(nameof (DoctorsID))]
        public virtual ApplicationUser? Doctor { get; set; }
         public string? Notes { get; set; }
      


    }
    public enum CaseStatus
    {
        Open,
        UnderInvestigation,
        InterventionInProgress,
        Closed,
        Other
    }
}

