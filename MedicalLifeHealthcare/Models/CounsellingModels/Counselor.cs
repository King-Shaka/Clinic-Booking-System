using System.ComponentModel.DataAnnotations;

namespace MedicalLifeHealthcare.Models.CounsellingModels
{
    public class Counselor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }

        // Additional counselor-related properties
        public bool IsActive { get; set; }
        public string Education { get; set; }
        public int YearsOfExperience { get; set; }
        public string LicenseNumber { get; set; }
        public string LanguagesSpoken { get; set; }
        public string OfficeAddress { get; set; }

        // Relationships
        public virtual ICollection<Appointment> Appointments { get; set; }
        // Other relationships as needed
    }
}
