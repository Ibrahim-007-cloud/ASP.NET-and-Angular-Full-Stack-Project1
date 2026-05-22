namespace ClinicManagement.Domain.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        
        public string Problem { get; set; } = string.Empty;
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
    }
}