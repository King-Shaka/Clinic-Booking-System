using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }
        public string? Message { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string? Status { get; set; }
        public string? Purpose { get; set; }
        public string? IntendedUser { get; set; }
        [ForeignKey("IntendedUser")]
        public virtual ApplicationUser? CurrentUser { get; set; }


        public Alert()
        {
            Status = "New";
            date = DateTime.Now;
            Time = DateTime.Now;
            IntendedUser = "All";
            Purpose = "All";

        }
    }
}
