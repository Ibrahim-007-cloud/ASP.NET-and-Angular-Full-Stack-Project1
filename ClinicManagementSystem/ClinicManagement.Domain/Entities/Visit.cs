using System;

namespace ClinicManagement.Domain.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Problem { get; set; } = string.Empty; 
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}