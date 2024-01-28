using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Medical_Records
    {
        [Key]
        public int RecordsID { get; set; }
        public int FileID { get; set; }
        [ForeignKey(nameof(FileID))]
        public virtual Medical_File? File { get; set; }
        [Required]
        [Display(Name = "Current Meds")]
        // Vital Signs and Measurements
        public string? CurrentMedications { get; set; }
        [Required]
        [Display(Name = "Blood pressure (mmHg)")]
        public decimal? BloodPressure { get; set; }
        [Required]
        [Display(Name = "Heart rate (BPM)")]
        public int? HeartRate { get; set; }
        [Required]
        [Display(Name = "Temparature (°C)")]
        public decimal? Temperature { get; set; }
        [Required]

        [Display(Name = "Height (Cm)")]
        public decimal? Height { get; set; }
        [Required]
        [Display(Name = "Weight (Kg)")]
        public decimal? Weight { get; set; }
        [Required]
        // Notes and Observations
        public string? Notes { get; set; }

        //Date OF Birth

        [Display(Name = "Record Date")]
        public DateTime RecordDate { get; set; }

        public string? NurseID { get; set; }
        [ForeignKey(nameof(NurseID))]
        public virtual ApplicationUser? Nurse { get; set; }

        public Medical_Records()
        {
            RecordDate = DateTime.Now;
        }
    }
}
