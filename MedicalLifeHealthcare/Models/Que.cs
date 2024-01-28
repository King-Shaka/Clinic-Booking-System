using MedicalLifeHealthcare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalLifeHealthcare.Models
{
    public class Que
    {
        [Key]
        public int QueID { get; set; }
        public int AppointmentID { get; set; }
        [ForeignKey(nameof(AppointmentID))]
        public virtual Appointments? Appointments { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime dateOFQue { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name ="Delay Time")]
        public DateTime Time { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public string? RoomNumber { get; set; }
        [Display(Name = "Clinician Infromation")]
        public string? ClinicianID { get; set; }
        [ForeignKey(nameof (ClinicianID))]
        public virtual ApplicationUser? Clinician { get; set; }
        public string? Status { get; set; }

        public Que()
        {
            dateOFQue = DateTime.Now;
            Status = "On Que";
        }

    }
}
