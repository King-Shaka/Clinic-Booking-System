using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Referal
    {
        [Key]
        public int ReferalId { get; set; }
        public int CaseID { get; set; }
        [ForeignKey(nameof(CaseID))]
        public virtual Case? CaseInfor { get; set; }

        [Required]
        public string? ReferallType {  get; set; }
        public string? Notes { get; set; }
        public string? DoctorID { get; set; }
        [ForeignKey(nameof(DoctorID))]
        public virtual ApplicationUser? Doctor { get; set; }

    }
}
