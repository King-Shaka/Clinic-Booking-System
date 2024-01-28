using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MedicalLifeHealthcare.Models
{
    public class IncidentReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string? PatientID { get; set; } // Nullable in case anonymity is desired
        public virtual ApplicationUser? Patient { get; set; }

        public string? Type { get; set; }
        public string? Description { get; set; } // Detailed account of the incident

        // Location details (can be made more detailed, for instance, using an Address model)
        public string? Location { get; set; }
        public DateTime? InciodentDate { get; set; }
        // Any immediate actions taken or recommendations provided
        public string? ActionsTaken { get; set; }
        public string? Status { get; set; } = "New";
    }

 

    //public enum GBVType
    //{
    //    Physical,
    //    Sexual,
    //    Psychological,
    //    Economic,
    //    Cyberbullying,
    //    Stalking,
    //    Other
    //}

   
}
