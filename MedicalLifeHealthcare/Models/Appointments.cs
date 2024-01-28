using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.Build.Execution;
using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace MedicalLifeHealthcare.Models
{
    public class Appointments
    {
        [Key]
        public int AppointmentID { get; set; }
        [DataType(DataType.DateTime)]   
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reason for appointment should be included")]
        public string? Reason { get; set; }
        public string? Status { get; set; } = "New";
        public String? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual ApplicationUser? MainUser { get; set; }  
        [Required]
        [Display(Name = "Appointment Date and Time")]
        [DataType(DataType.DateTime)]
        public DateTime Date_Time { get; set; } 
    }
}
