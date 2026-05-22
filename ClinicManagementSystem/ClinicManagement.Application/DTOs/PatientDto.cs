using System;

namespace ClinicManagement.Application.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string LastProblem { get; set; } = string.Empty;
        public string AssignedDoctor { get; set; } = string.Empty;
        public DateTime? LastVisitDate { get; set; }
    }
}