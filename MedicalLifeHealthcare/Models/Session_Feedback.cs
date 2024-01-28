using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Session_Feedback
    {
        [Key]
        public int FeedbackID { get; set; }
        public int SessionID { get; set; }
		[ForeignKey("SessionID")]
        public virtual Counselling_Sessions? Session { get; set; }
        [Required]
        public string? Feedback { get;set; }
        public string? Counsellor_Feedback { get; set; }
        public DateTime? FeedbackDate { get; set; } = DateTime.Now;
        public string? Satus { get; set; } = "New";



	}
}
