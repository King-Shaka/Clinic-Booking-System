namespace MedicalLifeHealthcare.Models
{
    public class GBVReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string VictimName { get; set; }
        public string PerpetratorName { get; set; }
        public string IncidentDetails { get; set; }
    }
}
